using System;
using System.Collections.Generic;
using System.Linq;

namespace NetBDI.STRIPS
{
    /// <summary>
    /// The planner responsible for generating a plan given a complex goal with multiple subgoals.
    /// </summary>
    public static class Planner
    {
        /// <summary>
        /// Creates an executable plan based on a stack of items that contains goals and actions, based on the current set of beliefs 
        /// and based on a list of potentially executable actions. To avoid loops, we also keep a list of previously found preconditions 
        /// and previously visited states. Based on the the last parameter (default true), we can adapt the planner so that it returns the 
        /// optimal plan or simply the first executable plan that fulfills all goals.
        /// </summary>
        /// <param name="itemsToProcess">The items to process. Only if all items have been processed, is the actual filled plan returned.</param>
        /// <param name="currentBeliefs">The current set of beliefs</param>
        /// <param name="preconditions">Previously encountered preconditions to be used in loop detection</param>
        /// <param name="previousStates">Previously visited states used in loop detection</param>
        /// <param name="actions">The list of templates of actions to be performed by the agent and to be used by the planner</param>
        /// <param name="optimal">If true, the planner will look for the most optimal plan. Otherwise it will return the first viable plan it finds.</param>
        /// <returns>An executable plan (if one exists) and the set of beliefs after the plan has been executed. Null otherwise. Optimality of the plan depends on the optimal flag.</returns>
        public static (Plan<TAction>, List<Fact>) CreatePlan<TAction>(Stack<IStackItem> itemsToProcess, List<Fact> currentBeliefs, List<Fact> preconditions, List<State> previousStates, List<Action> actions, bool optimal = true) where TAction : Action
        {
            var plan = new Plan<TAction>();
            if (itemsToProcess.Any())
            {
                var currentItem = itemsToProcess.Pop();
                if (currentItem is ComplexGoal currentGoal)
                {
                    //decompose the complex goal, return null when something goes wrong
                    if (!DecomposeAndCheckComplexGoal(currentGoal, currentBeliefs, previousStates, itemsToProcess))
                        return (null, null);
                }
                else if (currentItem is SimpleGoal simpleGoal)
                {
                    //first substitution, then fulfill the goal, return null when something goes wrong
                    if (!PerformAndCheckSubstitionIfNeeded(simpleGoal, currentBeliefs, preconditions))
                        return (null, null);

                    //fulfill the goal when not fulfilled, return null when something goes wrong
                    if (!FulFillSimpleGoal(simpleGoal, plan, optimal, itemsToProcess, previousStates, currentBeliefs, preconditions, actions))
                        return (null, null);
                }
                else if (currentItem is TAction currentAction)
                    ProcessAction(plan, currentAction, ref preconditions, currentBeliefs);

                //create a subplan (if one exists) for all items remaining in the stack and add the steps of that plan to the current 
                //plan and return it
                (Plan<TAction> subPlan, List<Fact> lstBelief) = CreatePlan<TAction>(itemsToProcess, currentBeliefs, preconditions, previousStates, actions, optimal);
                if (subPlan != null)
                {
                    plan.Steps.AddRange(subPlan.Steps);
                    return (plan, lstBelief);
                }
                else
                    return (null, null);
            } 
            else
                //No more items to process so we're done and return the current plan
                return (plan, currentBeliefs);
        }

        /// <summary>
        /// Fulfill a simple goal when needed. This is done by finding an action that has a more general version of the goal in its add list.
        /// If we find an action, we instantiate it and add it along with its preconditions to the stack (preconditions first).
        /// Some loopdetection is performed to see if we haven't already added a precondition of the action as this indicates a loop.
        /// Multiple actions are possible so if choosing one action ends in an unplannable situation (due to a loop or no choice of actions further down the line)
        /// we simply backtrack to the previous choice of actions and choose another. When looking for an optimal path, this backtracking is done for every choice so
        /// we can guarantee that we make the most optimal choice at any point of the execution.
        /// </summary>
        /// <param name="simpleGoal">The goal we wish to see fulfilled by choosing an action</param>
        /// <param name="plan">The current plan</param>
        /// <param name="optimal">A flag indicating if we're looking for the optimal plan or not</param>
        /// <param name="itemsToProcess">The stack of items to process containing goals and actions</param>
        /// <param name="previousStates">A list of previous states to be used in loop detection</param>
        /// <param name="currentBeliefs">The current set of beliefs</param>
        /// <param name="preconditions">A list of preconditions used in loop detection</param>
        /// <param name="actions">A list of executable actions of the agent</param>
        /// <returns>True if we find a plan that fulfills the goal and all remaining items in the stack, false otherwise</returns>
        private static bool FulFillSimpleGoal<TAction>(SimpleGoal simpleGoal, Plan<TAction> plan, bool optimal, Stack<IStackItem> itemsToProcess, 
            List<State> previousStates, List<Fact> currentBeliefs, List<Fact> preconditions, List<Action> actions) where TAction : Action
        {
            //only look for an action when the goal hasn't been achieved yet
            if (!simpleGoal.IsFulFilled(currentBeliefs))
            {
                //add the fact of the goal in the list of visited preconditions
                preconditions.Add(simpleGoal.Fact);
                //look for possible actions to execute
                var possibleActions = actions.Where(x => x.IsApplicableFor(simpleGoal, currentBeliefs));
                var iter = possibleActions.GetEnumerator();

                //keep a list of plans and beliefsets for each action that is possible (for optimization later)
                var subPlansForAction = new List<Tuple<Plan<TAction>, List<Fact>>>();
                while (iter.MoveNext())
                {
                    var action = iter.Current;
                    //instantiate the action as much as possible
                    action = action.InstantiateFor(simpleGoal, currentBeliefs);

                    //perform loopdetection
                    var lstPre = action.Preconditions.ToList();
                    lstPre.Reverse();
                    var faulty = lstPre.Any(x => IsAlreadyACondition(x, preconditions));
                    if (!faulty)
                    {
                        //create the complex goal with the preconditions of the action
                        var complex = new ComplexGoal();
                        foreach (var precondition in lstPre)
                            complex.Goals.Add(new SimpleGoal(precondition, action));

                        //create a new stack but with action and complex goal added
                        var newStack = new Stack<IStackItem>(itemsToProcess.Reverse());
                        newStack.Push(action);
                        newStack.Push(complex);

                        //perform create plan for the new stack
                        (Plan<TAction> subPlanForAction, List<Fact> lstBeliefs) = CreatePlan<TAction>(newStack, currentBeliefs.ToList(), preconditions.ToList(), 
                            previousStates.ToList(), actions, optimal);

                        //the chosen action gives a valid plan so add the plan (and beliefset) to the possible plans we can choose from
                        if (subPlanForAction != null)
                        {
                            subPlansForAction.Add(Tuple.Create(subPlanForAction, lstBeliefs));
                            if (optimal)
                                break;
                        }
                    }
                }
                //no plan found for this goal and current beliefset so return null
                if (!subPlansForAction.Any())
                    return false;
                else
                {
                    //we look for the minimal path (minimal number of steps) asuming that each step has an equal 'cost'
                    var minSteps = subPlansForAction.Min(x => x.Item1.Steps.Count());
                    var best = subPlansForAction.First(x => x.Item1.Steps.Count() == minSteps);
                    //add the steps of the optimal subplan to this plan and update the current set of beliefs
                    plan.Steps.AddRange(best.Item1.Steps);
                    currentBeliefs = best.Item2;
                    //clear the stack as we already processed all items in the stack when looking for all subplans
                    itemsToProcess.Clear();
                }
            }
            return true;
        }

        /// <summary>
        /// Perform and check the subsitution but only when needed
        /// </summary>
        /// <param name="simpleGoal">The goal to perform substitution on</param>
        /// <param name="currentBeliefs">The current set of beliefs</param>
        /// <param name="preconditions">The current list of preconditions used when performing loop detection</param>
        /// <returns>True if no loops detected and substitution is possible when it is required, false otherwise</returns>
        private static bool PerformAndCheckSubstitionIfNeeded(SimpleGoal simpleGoal, List<Fact> currentBeliefs, List<Fact> preconditions)
        {
            if (simpleGoal.NeedsSubstitution())
            {
                var toSubstitute = FindGoodSubstitution(simpleGoal, currentBeliefs);
                //no substitution found so no plan possible for the current choice of actions
                if (toSubstitute == null)
                    return false;

                simpleGoal.Action.FillParameters(toSubstitute);
                //simple loop detection
                if (IsAlreadyACondition(simpleGoal.Fact, preconditions))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Processing of an action in the stack
        /// </summary>
        /// <param name="plan">The plan with the steps that have been planned so far</param>
        /// <param name="currentAction">The action being processed</param>
        /// <param name="preconditions">The list of preconditions for loop detection</param>
        /// <param name="currentBeliefs">The current set of beliefs</param>
        private static void ProcessAction<TAction>(Plan<TAction> plan, TAction currentAction, ref List<Fact> preconditions, List<Fact> currentBeliefs) where TAction : Action
        {
            //add the action to the plan
            plan.Steps.Add(currentAction);
            //reset the preconditions
            preconditions = new List<Fact>();
            //addlist added to current set of beliefs and deletelist removed from current set of beliefs
            var toDelete = currentBeliefs.Where(x => currentAction.DeleteList.Any(y => y.IsSameAs(x))).ToList();
            foreach (var del in toDelete)
                currentBeliefs.Remove(del);

            currentBeliefs.AddRange(currentAction.AddList);
        }

        /// <summary>
        /// Process and check the complex goal for loop detection
        /// </summary>
        /// <param name="currentGoal">The complex goal to be checked and processed</param>
        /// <param name="currentBeliefs">The current set of beliefs</param>
        /// <param name="previousStates">The list of previous states</param>
        /// <param name="itemsToProcess">The stack of items to process</param>
        /// <returns>True if the complex goal does not contain loops and is processed, false otherwise</returns>
        private static bool DecomposeAndCheckComplexGoal(ComplexGoal currentGoal, List<Fact> currentBeliefs, List<State> previousStates, Stack<IStackItem> itemsToProcess)
        {
            //only if the complex goal is not fulfilled, do we need to process and check it
            if (!currentGoal.IsFulFilled(currentBeliefs))
            {
                //if a loop is detected, we end execution, else we add this state to the list of states
                if (CheckForLoop(currentBeliefs, currentGoal, previousStates))
                    return false;
                else
                    previousStates.Add(new State { CurrentComplexGoal = currentGoal, CurrentBeliefs = currentBeliefs });

                //we repush the complex goal in the items to process so it can be checked again after all subgoals have been fulfilled
                //this is done to avoid that the solution to a subgoal 'destroys' another fulfilled subgoal
                itemsToProcess.Push(currentGoal);
                //all subgoals are pushed on the stack
                foreach (var simpleGoal in currentGoal.Goals)
                    itemsToProcess.Push(simpleGoal);
            }
            return true;
        }

        /// <summary>
        /// Check for a loop in the execution of our planner so that we do not introduce infinite loops.
        /// We test this by testing if we encountered the same current beliefs and the same complex goal somewhere before.
        /// If so, this indicated that we're stuck in a loop and we need to exit the current choice of actions to find a proper path
        /// of steps/actions that lead to the fulfillment of all given goals.
        /// </summary>
        /// <param name="currentBeliefs">The set of facts we current believe to be true</param>
        /// <param name="goal">The complex goal currently being processed</param>
        /// <param name="states">The list of previous states we encountered</param>
        /// <returns>True if a state can be found that contains the same given goal and where the set of beliefs of that state
        /// completely matches the current set of beliefs.</returns>
        private static bool CheckForLoop(List<Fact> currentBeliefs, ComplexGoal goal, List<State> states)
        {
            foreach(var state in states)
                if (goal.IsSame(state.CurrentComplexGoal) && state.CurrentBeliefs.Count() == currentBeliefs.Count() 
                    && state.CurrentBeliefs.All(x => currentBeliefs.Any(y => y.IsSameAs(x))))
                    return true;

            return false;
        }

        /// <summary>
        /// Check to see if the given condition is already present in the current set of conditions we must fulfill.
        /// This is mainly used for a quick loop detection
        /// </summary>
        /// <param name="condition">The condition to check for existence</param>
        /// <param name="conditions">The list of conditions that may contain the given condition</param>
        /// <returns>True if the given condition already exists in the list of conditions, false otherwise.</returns>
        private static bool IsAlreadyACondition(Fact condition, List<Fact> conditions) => 
            conditions.Any((prec) => !condition.NeedsSubstitution() && prec.IsSameAs(condition));

        /// <summary>
        /// Finds a substitution/assigment for a fact of a goal so that the instantiated fact exists in the current beliefset
        /// </summary>
        /// <param name="goal">The goal to be fulfilled</param>
        /// <param name="currentBeliefs">The current beliefset</param>
        /// <returns>An assignment/substitution for the fact of the goal such that the instantiated fact exists in the current beliefset</returns>
        private static Dictionary<string, object> FindGoodSubstitution(SimpleGoal goal, List<Fact> currentBeliefs)
        {
            var dict = new Dictionary<string, object>();
            //first look for candidate facts in the current beliefset that bear the same name as the fact in the goal
            var candidateFacts = currentBeliefs.Where(x => x.Name.Equals(goal.Fact.Name));
            var lstOk = new List<Fact>();
            //keep a list of those facts in the reduced list that have the same value in their parameters as the goal has when it has
            //filled in valueparameters instead of named parameters
            foreach (var candidateFact in candidateFacts)
            {
                var ok = true;
                for (var i = 0; i < candidateFact.Parameters.Count && ok; i++)
                {
                    var canParam = candidateFact.Parameters[i] as ValueParameter;
                    if (goal.Fact.Parameters[i] is ValueParameter valParam && !((valParam.Value == null && canParam.Value == null) || (valParam.Value != null && (valParam.Value == canParam.Value || valParam.Value.Equals(canParam.Value)))))
                        ok = false;
                }

                if (ok)
                    lstOk.Add(candidateFact);
            }
            //get the first in this list (or null) :=> REMARK : possibly change this as this is inefficient
            var firstOk = lstOk.FirstOrDefault();
            if (firstOk == null)
                return null;

            //for the found fact, calculate the values for the remaining named parameters and fill the subsitution with this information
            for (var i = 0; i < firstOk.Parameters.Count; i++)
                if (goal.Fact.Parameters[i] is NamedParameter namedParam)
                    dict.Add(namedParam.Name, ((ValueParameter)firstOk.Parameters[i]).Value);

            return dict;
        }
    }
}