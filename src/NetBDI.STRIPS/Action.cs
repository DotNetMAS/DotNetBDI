using System;
using System.Collections.Generic;
using System.Linq;

namespace NetBDI.STRIPS
{
    /// <summary>
    /// An action that can be performed by an agent.
    /// An action contains a set of preconditions that act as a list of 
    /// facts that need to be true in the beliefset for the action to be able to be performed.
    /// An action also contains an add and delete list that are also a list of actions. At the end of the action, the facts of the add list are 
    /// added to the current beliefset while the facts in the delete list are removed from the beliefset
    /// On top of this, we also define constraints for our parameters.  
    /// This is important when looking for an action that potentially fulfills a goal but also when performing the actual substitution.
    /// At all times, the values for the parameters must be chosen so that no constraints are violated.
    /// </summary>
    public abstract class Action : IStackItem
    {
        /// <summary>
        /// The constraints of the parameters for this action. This is kept as a list of functions that can receive a substitution
        /// (in the form of a list of keyvalue pairs of names and values) and return true if not violated and false otherwise
        /// </summary>
        public List<Func<Dictionary<string, object>, bool>> Constraints { get; } = new List<Func<Dictionary<string, object>, bool>>();
        /// <summary>
        /// A list of preconditions as a list of facts
        /// </summary>
        public List<Fact> Preconditions { get; } = new List<Fact>();
        /// <summary>
        /// A list of add-predicates in the form of a list of facts
        /// </summary>
        public List<Fact> AddList { get; } = new List<Fact>();
        /// <summary>
        /// A list of delete-predicates in the form of a list of facts
        /// </summary>
        public List<Fact> DeleteList { get; } = new List<Fact>();

        public Dictionary<string, object> Assignments = new Dictionary<string, object>();

        /// <summary>
        /// A constraint that can be used to check if a the value of a parameter is of a certain type
        /// </summary>
        /// <param name="expectedType">The expected type of the value of a parameter</param>
        /// <param name="namedParameter">The named parameter being checked</param>
        /// <returns>A function that returns true if the value in the substitution corresponding to the given name is of the expected type, false otherwise</returns>
        public static Func<Dictionary<string, object>, bool> TypeCheck(Type expectedType, NamedParameter namedParameter)
        {
            return (tuples) => !tuples.ContainsKey(namedParameter.Name) || (tuples[namedParameter.Name] != null && tuples[namedParameter.Name].GetType().Equals(expectedType));
        }

        /// <summary>
        /// A constraint that can be used to check if two parameters do not contain the same value
        /// </summary>
        /// <param name="namedParam1">The first named parameter being checked for equality</param>
        /// <param name="namedParam2">The other named parameter being checked for equality</param>
        /// <returns>True if one of the parameters is not present in the substitution or if the values of both parameters are equal, false otherwise</returns>
        public static Func<Dictionary<string, object>, bool> NotEqualsCheck(NamedParameter namedParam1, NamedParameter namedParam2)
        {
            return (tuples) => !tuples.ContainsKey(namedParam1.Name) || !tuples.ContainsKey(namedParam2.Name) 
                || (tuples[namedParam1.Name] == null && tuples[namedParam2.Name] == null) 
                || (tuples[namedParam1.Name] !=null && !tuples[namedParam1.Name].Equals(tuples[namedParam2.Name]));
        }

        /// <summary>
        /// Check to see if an action is applicable to fulfill a certain goal
        /// </summary>
        /// <param name="goal">The goal to be tested for potential fulfillment</param>
        /// <param name="currentBeliefs">The current set of beliefs</param>
        /// <returns>True if the action is applicable to fulfill the goal, false otherwise</returns>
        internal bool IsApplicableFor(SimpleGoal goal, List<Fact> currentBeliefs) => IsApplicableForAdd(goal, currentBeliefs) != null;

        /// <summary>
        /// Instantiate an action so an instantiated goal is fulfilled
        /// </summary>
        /// <param name="goal">The goal to be fulfilled</param>
        /// <param name="currentBeliefs">The current set of beliefs</param>
        /// <returns>The instantiated action (based on the given action) that completely fulfills the given goal</returns>
        internal Action InstantiateFor(SimpleGoal goal, List<Fact> currentBeliefs)
        {
            var cloned = Clone();
            var fact = cloned.IsApplicableForAdd(goal, currentBeliefs);
            var tuples = GetAssignment(fact, goal, currentBeliefs);
            cloned.FillParameters(tuples);
            return cloned;
        }

        /// <summary>
        /// Fills the parameters of all properties of the action according to a substitution in the form of a list 
        /// of keyvalue pairs of names and values. All preconditions but also the add- and deletelist are filled.
        /// </summary>
        /// <param name="substitution">The list of keyvalue pairs that are the basis for a substitution</param>
        internal void FillParameters(Dictionary<string, object> substitution)
        {
            foreach (var prec in Preconditions)
                prec.Substitute(substitution);

            foreach (var add in AddList)
                add.Substitute(substitution);

            foreach (var delete in DeleteList)
                delete.Substitute(substitution);

            foreach (var subs in substitution)
                Assignments.Add(subs.Key, subs.Value);
        }

        /// <summary>
        /// Each action needs to be cloneable as each action can act as a template for a fully instantiated action
        /// </summary>
        /// <returns>A clone of the action that is of the correct type</returns>
        public abstract Action Clone();

        /// <summary>
        /// Checks the constraints for a given substitution (in the form of a list of keyvalue pairs of names and values)
        /// </summary>
        /// <param name="substititution">The substitution to be tested against the constraints of the action</param>
        /// <returns>True if the substitution fulfills all constraints, false otherwise</returns>
        private bool CheckConstraints(Dictionary<string, object> substititution) => Constraints.All(x => x(substititution));

        /// <summary>
        /// Check to see if the add list of the action can potentially be used to fulfill a goal
        /// </summary>
        /// <param name="goal">The goal to be fulfilled</param>
        /// <param name="currentBeliefs">The current beliefs that we have at the moment</param>
        /// <returns>Returns a fact that can be found in the add list that has the same name as the end goal/fact in the goal 
        /// and where an assignment can be found that fulfills the goal and does not violate the constraints of the action.</returns>
        internal virtual Fact IsApplicableForAdd(SimpleGoal goal, List<Fact> currentBeliefs)
        {
            //only facts that have the same name as the goal are considered but also only those that are more general (or equally general) than the goal
            //this means that a substitution (or empty substitution) exists that transforms the fact of the addlist into the fact of the goal
            var possibeAdds = AddList.Where(y => y.Name.Equals(goal.Fact.Name) && y.IsMoreGeneralThanOrEqualTo(goal.Fact));
            //if no such facts can be found, quickly return null
            if (!possibeAdds.Any())
                return null;

            //if there are no constraints, quickly return the first fact that can fulfill the goal
            if (!Constraints.Any())
                return possibeAdds.FirstOrDefault();

            //return the first fact of the possible facts of the addlist where an assignment can be found that also 
            //does not violate the constraints of the action
            foreach (var possibleGoal in possibeAdds)
            {
                var tuples = GetAssignment(possibleGoal, goal, currentBeliefs);
                if (CheckConstraints(tuples))
                    return possibleGoal;
            }
            return null;
        }

        /// <summary>
        /// Returns a potential assignment/substitution in the form of a list of keyvalue pair of names and values that
        /// fulfills the goal based on the given fact of the addlist
        /// </summary>
        /// <param name="possibleAdd">The fact of the addlist that can be instantiated to fulfill the goal.</param>
        /// <param name="goal">The goal we wish to see fulfilled</param>
        /// <param name="currentBeliefs">The current beliefs that we have at the moment</param>
        /// <returns>A potential assignment for the fact of the addlist to fulfill the given goal.</returns>
        public virtual Dictionary<string, object> GetAssignment(Fact possibleAdd, SimpleGoal goal, List<Fact> currentBeliefs)
        {
            var tuples = new Dictionary<string, object>();
            for (var i = 0; i < Math.Min(goal.Fact.Parameters.Count, possibleAdd.Parameters.Count); i++)
                if (possibleAdd.Parameters[i] is NamedParameter namedParameter)
                    tuples.Add(namedParameter.Name, (goal.Fact.Parameters[i] as ValueParameter).Value);

            return tuples;
        }
    }
}