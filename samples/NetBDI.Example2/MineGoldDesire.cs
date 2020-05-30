using NetBDI.Core;
using NetBDI.STRIPS;
using System.Linq;

namespace NetBDI.Example2
{
    /// <summary>
    /// THe desire to mine gold
    /// </summary>
    public class MineGoldDesire : Desire<GoldMineAction, GoldMineAgent, GoldMineEnvironment>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MineGoldDesire() : base(2)
        {
            Precondition = HasNoGoldAndThereIsAGoldMine;
        }

        /// <summary>
        /// We create a complex goal
        /// </summary>
        /// <returns>A complex goal</returns>
        public override ComplexGoal CreateGoal()
        {
            var complexGoal = new ComplexGoal();
            complexGoal.Goals.Add(new SimpleGoal(new Fact(Definitions.HasGold, new ValueParameter(true))));
            return complexGoal;
        }

        /// <summary>
        /// Check to see if we're not carrying gold and there is a gold mine
        /// </summary>
        /// <returns>True if we're not carrying gold and there is a mine</returns>
        public bool HasNoGoldAndThereIsAGoldMine() =>
            !Agent.BeliefBase.GetBelief("hasgold").GetValue<bool>() && Agent.BeliefBase.GetBeliefSet("mines").Values.Count() > 0;
    }
}