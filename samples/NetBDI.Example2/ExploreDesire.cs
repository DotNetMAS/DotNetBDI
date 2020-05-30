using NetBDI.Core;
using NetBDI.STRIPS;
using System;

namespace NetBDI.Example2
{
    /// <summary>
    /// The desire to find gold
    /// </summary>
    public class ExploreDesire : Desire<GoldMineAction, GoldMineAgent, GoldMineEnvironment>
    {
        /// <summary>
        /// The x position the agent wishes to go to
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The y position the agent wishes to go to
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Default constructor, we let the agent wander
        /// </summary>
        public ExploreDesire() : base(1)
        {
            Wander();
            TargetCondition = CreateNewPointOnTarget;
        }

        /// <summary>
        /// Create a complexgoal
        /// </summary>
        /// <returns>A complexgoal</returns>
        public override ComplexGoal CreateGoal()
        {
            var complexGoal = new ComplexGoal();
            complexGoal.Goals.Add(new SimpleGoal(new Fact(Definitions.In, new ValueParameter(X), new ValueParameter(Y))));
            return complexGoal;
        }

        /// <summary>
        /// This desire is never finished but the targetcondition is called when the current intention has been fulfilled thus we can choose a new point
        /// </summary>
        /// <returns>Returns false</returns>
        private bool CreateNewPointOnTarget()
        { 
            if (Agent.BeliefBase.GetBelief("inx").GetValue<double>() == X && Agent.BeliefBase.GetBelief("iny").GetValue<double>() == Y)
                Wander();

            return false;
        }

        /// <summary>
        /// We let the agent wander randomly in the field
        /// </summary>
        private void Wander()
        {
            var random = new Random();
            X = random.Next(0, 20);
            Y = random.Next(0, 20);
        }
    }
}