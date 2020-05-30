using NetBDI.Core;
using NetBDI.STRIPS;

namespace NetBDI.Example2
{
    /// <summary>
    /// The desire to sell gold
    /// </summary>
    public class SellGoldDesire : Desire<GoldMineAction, GoldMineAgent, GoldMineEnvironment>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SellGoldDesire() : base(2)
        {
            Precondition = HasGold;
        }

        /// <summary>
        /// We create a complex goal
        /// </summary>
        /// <returns>The complex goal created</returns>
        public override ComplexGoal CreateGoal()
        {
            var complexGoal = new ComplexGoal();
            complexGoal.Goals.Add(new SimpleGoal(new Fact(Definitions.HasGold, new ValueParameter(false))));
            return complexGoal;
        }

        /// <summary>
        /// Check to see if we're holding gold
        /// </summary>
        /// <returns>True if we have gold, false otherwise</returns>
        private bool HasGold() => Agent.BeliefBase.GetBelief("hasgold").GetValue<bool>();
    }
}