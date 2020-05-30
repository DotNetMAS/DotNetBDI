using NetBDI.STRIPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetBDI.Core
{
    /// <summary>
    /// An agent having possible actions, beliefbase, intentions, desires and operating in an environment
    /// </summary>
    public abstract class Agent<TAction, TAgent, TEnvironment>
        where TAction : STRIPS.Action
        where TAgent : Agent<TAction, TAgent, TEnvironment>
        where TEnvironment : IEnvironment
    {
        //Asynchronous task setup
        private CancellationToken _token;
        private readonly CancellationTokenSource _source = new CancellationTokenSource();

        //The commitmenttype of this agent
        private readonly CommitmentType _commitmentType = CommitmentType.Blind;

        /// <summary>
        /// A list of desires of the agent
        /// </summary>
        protected List<Desire<TAction, TAgent, TEnvironment>> Desires { get; private set; } = new List<Desire<TAction, TAgent, TEnvironment>>();

        /// <summary>
        /// The possible actions of an agent. They act as templates for planning
        /// </summary>
        protected List<STRIPS.Action> Actions { get; } = new List<STRIPS.Action>();

        /// <summary>
        /// The environment of the agent
        /// </summary>
        public TEnvironment Environment { get; set; }

        /// <summary>
        /// The current intentions of the agent
        /// </summary>
        public List<Intention<TAction, TAgent, TEnvironment>> CurrentIntentions { get; private set; } = new List<Intention<TAction, TAgent, TEnvironment>>();

        /// <summary>
        /// The beliefbase (current set of beliefs) of this  agent
        /// </summary>
        public BeliefBase BeliefBase { get; } = new BeliefBase();

        /// <summary>
        /// Default constructor which requires an environment and an optional commitmenttype
        /// </summary>
        /// <param name="environment">The environment of the agent</param>
        /// <param name="comType">The commimenttype of the agent and if not given, is set to blind commitment</param>
        public Agent(TEnvironment environment, CommitmentType? comType = null)
        {
            Environment = environment;
            if (comType.HasValue)
                _commitmentType = comType.Value;
        }

        /// <summary>
        /// Add a desire to the agent
        /// </summary>
        /// <param name="desire">The desire to add</param>
        public void AddDesire(Desire<TAction, TAgent, TEnvironment> desire)
        {
            Desires.Add(desire);
            desire.Agent = this;
        }

        /// <summary>
        /// Initializes the agent
        /// </summary>
        /// <param name="desires">The initial list of desires of the agent</param>
        /// <param name="intentions">The initial current intentions of the agent</param>
        /// <returns>A task in which the agent loop is run</returns>
        public virtual Task Init(List<Desire<TAction, TAgent, TEnvironment>> desires, List<Intention<TAction, TAgent, TEnvironment>> intentions)
        {
            if (desires != null)
            {
                Desires.AddRange(desires);
                foreach (var desire in desires)
                    desire.Agent = this;
            }

            if (intentions != null)
                CurrentIntentions.AddRange(intentions);

            return StartRun();
        }

        /// <summary>
        /// We can stop the current agent from interacting
        /// </summary>
        public void Stop() => _source.Cancel();

        /// <summary>
        /// The agent will perceive the environment and returns its percepts
        /// </summary>
        /// <returns>Percepts in the form of a list of keyvaluepairs with the name of the percept and the actual value for that percept</returns>
        protected abstract Dictionary<string, object> See();

        /// <summary>
        /// An update of the beliefs of the agent
        /// </summary>
        /// <param name="percepts">The new percepts of the agent to be processed</param>
        protected abstract void UpdateBeliefs(Dictionary<string, object> percepts);

        /// <summary>
        /// The belief revision in which the agent perceives the environment and updates its beliefs
        /// </summary>
        protected void BeliefRevision() => UpdateBeliefs(See());

        /// <summary>
        /// Creation of a plan for the current intention
        /// </summary>
        /// <returns>A STRIPS plan which can be executed</returns>
        protected virtual Plan<TAction> MakePlan()
        {
            var itemsToProcess = new Stack<IStackItem>();
            var complexGoal = new ComplexGoal();
            foreach(var intent in CurrentIntentions)
                complexGoal.Goals.AddRange(intent.CreateGoal().Goals);

            itemsToProcess.Push(complexGoal);
            var currentBeliefs = ExtractFactsFromBeliefBase();
            var tuple = Planner.CreatePlan<TAction>(itemsToProcess, currentBeliefs, new List<Fact>(), new List<State>(), Actions);
            return tuple.Item1;
        }

        /// <summary>
        /// Function that extracts STRIPS facts from the current beliefbase
        /// </summary>
        /// <returns>A list of STRIPS facts</returns>
        protected abstract List<Fact> ExtractFactsFromBeliefBase();

        /// <summary>
        /// We generate options based on the current list of desires
        /// </summary>
        protected virtual IEnumerable<Desire<TAction, TAgent, TEnvironment>> GenerateOptions()
        {
            //remove desires which have been fulfilled
            Desires.RemoveAll(x => x.IsFulfilled());
            //only achievable desires
            return Desires.Where(x => x.IsAchievable());
        }

        /// <summary>
        /// Does our current intention needs reconsideration after we updated our beliefs
        /// </summary>
        /// <returns>True if we need to reconsider the current intention, false otherwise</returns>
        protected virtual bool NeedsReconsideration() => false;
        
        /// <summary>
        /// executes an action
        /// </summary>
        /// <param name="action">The action to execute</param>
        /// <returns>An asynchronous task performing the action</returns>
        protected abstract Task Execute(TAction action);

        /// <summary>
        /// Deliberation on the desires and current intention to come up with a new intention which is useful to pursue
        /// </summary>
        private void Deliberate()
        {
            var options = GenerateOptions();
            Filter(options);
        }

        /// <summary>
        /// Filter the desires to select a new current intention
        /// </summary>
        /// <param name="potentialDesires">Potential desires to filter out and use for intentions</param>
        private void Filter(IEnumerable<Desire<TAction, TAgent, TEnvironment>> potentialDesires)
        {
            //choose strongest achievable desire and add it as a base intention
            CurrentIntentions.Clear();
            potentialDesires = potentialDesires.OrderByDescending(y => y.Strength);

            //now add intentions that do not conflict
            foreach(var potentialDesire in potentialDesires)
                if (CurrentIntentions.All(x => x.IsCompatibleWith(potentialDesire)))
                    CurrentIntentions.Add(new Intention<TAction, TAgent, TEnvironment>(potentialDesire));
        }

        /// <summary>
        /// Starts the agent to run indefinitely
        /// </summary>
        /// <returns>The running task of this agent</returns>
        private Task StartRun()
        {
            _token = _source.Token;
            Func<object, Task> action = null;
            switch(_commitmentType)
            {
                case CommitmentType.Blind:
                    action = RunWithBlindCommitment;
                    break;
                case CommitmentType.OpenMinded:
                    action = RunWithOpenMindedCommitment;
                    break;
                case CommitmentType.SingleMinded:
                    action = RunWithSingleMindedCommitment;
                    break;
            }
            return action(_token);
        }

        /// <summary>
        /// Checks to see if our plan is still sound
        /// </summary>
        /// <param name="plan">our current plan</param>
        /// <returns>True if we can still execute the plan, false otherwise</returns>
        private bool Sound(Plan<TAction> plan)
        {
            var facts = ExtractFactsFromBeliefBase();
            foreach(var step in plan.Steps)
            {
                foreach (var prec in step.Preconditions)
                    if (!facts.Any(x => x.IsSameAs(prec)))
                        return false;

                foreach (var del in step.DeleteList)
                {
                    var toRemoves = facts.Where(x => x.IsSameAs(del));
                    foreach (var toRemove in toRemoves.ToList())
                        facts.Remove(toRemove);
                }

                foreach(var add in step.AddList)
                    facts.Add(add);
            }
            var complexGoal = CreateComplexGoalForIntentions();
            return complexGoal.IsFulFilled(facts);
        }

        /// <summary>
        /// Algorithm that will execute with blind commitment to the current selected intention
        /// </summary>
        /// <param name="obj">The cancellation token for the task</param>
        private async Task RunWithBlindCommitment(object obj)
        {
            var token = (CancellationToken)obj;
            //(checked token from time to time for cancelation to gracefully stop thread)
            while (!token.IsCancellationRequested)
            {
                BeliefRevision();
                Deliberate();
                if (CurrentIntentions.Any())
                {
                    var plan = MakePlan();
                    while (plan != null && plan.HasSteps() && !token.IsCancellationRequested)
                    {
                        var step = plan.GetFirstStep();
                        await Execute(step);
                        plan.FinishStep(step);
                        BeliefRevision();
                        if (plan.HasSteps() && !Sound(plan))
                            plan = MakePlan();
                    }
                }
                //before looping again, sleep 100ms
                await Task.Delay(100);
            }
        }

        /// <summary>
        /// Algorithm that will execute with single minded commitment to the current selected intention
        /// </summary>
        /// <param name="obj">The cancellation token for the task</param>
        private async Task RunWithSingleMindedCommitment(object obj)
        {
            var token = (CancellationToken)obj;
            //(checked token from time to time for cancelation to gracefully stop thread)
            while (!token.IsCancellationRequested)
            {
                BeliefRevision();
                Deliberate();
                if (CurrentIntentions.Any())
                {
                    var plan = MakePlan();
                    while (plan != null && plan.HasSteps() && !IntentionSucceeded() && !IntentionImpossible() && !token.IsCancellationRequested)
                    {
                        var step = plan.GetFirstStep();
                        await Execute(step);
                        plan.FinishStep(step);
                        BeliefRevision();
                        if (plan.HasSteps() && !Sound(plan))
                            plan = MakePlan();
                    }
                }
                //before looping again, sleep 100ms
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Algorithm that will execute with open minded commitment to the current selected intention
        /// </summary>
        /// <param name="obj">The cancellation token for the task</param>
        private async Task RunWithOpenMindedCommitment(object obj)
        {
            var token = (CancellationToken)obj;
            //(checked token from time to time for cancelation to gracefully stop thread)
            while (!token.IsCancellationRequested)
            {
                BeliefRevision();
                GenerateOptions();
                Deliberate();
                if (CurrentIntentions.Any())
                {
                    var plan = MakePlan();
                    while (plan != null && plan.HasSteps() && !IntentionSucceeded() && !IntentionImpossible() && !token.IsCancellationRequested)
                    {
                        var step = plan.GetFirstStep();
                        await Execute(step);
                        plan.FinishStep(step);
                        BeliefRevision();
                        if (NeedsReconsideration())
                            Deliberate();
                        if (plan.HasSteps() &&!Sound(plan))
                            plan = MakePlan();
                    }
                }
                //before looping again, sleep 100ms
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Check to see if our current intention has succeeded already
        /// </summary>
        /// <returns>True if we succeeded already in our current intention, false otherwise</returns>
        private bool IntentionSucceeded() => CurrentIntentions.All(x => x.IsFulfilled());

        /// <summary>
        /// Check to see if our current intention impossible?
        /// </summary>
        /// <returns>True if our current intention impossible to achieve, false otherwise</returns>
        private bool IntentionImpossible() => !CurrentIntentions.All(x => x.IsAchievable());

        private ComplexGoal CreateComplexGoalForIntentions()
        {
            var complexGoal = new ComplexGoal();
            foreach (var intent in CurrentIntentions)
                complexGoal.Goals.AddRange(intent.CreateGoal().Goals);

            return complexGoal;
        }
    }
}