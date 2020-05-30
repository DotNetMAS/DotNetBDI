using NetBDI.STRIPS;
using System;
using System.Threading.Tasks;

namespace NetBDI.Example2
{
    /// <summary>
    /// The action to sell gold
    /// </summary>
    public class SellGoldAction : GoldMineAction
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SellGoldAction()
        {
            var x = new NamedParameter("param1");
            var y = new NamedParameter("param2");
            Preconditions.Add(new Fact(Definitions.HasGold, new ValueParameter(true)));
            Preconditions.Add(new Fact(Definitions.Town, x, y));
            Preconditions.Add(new Fact(Definitions.In, x, y));
            AddList.Add(new Fact(Definitions.HasGold, new ValueParameter(false)));
            DeleteList.Add(new Fact(Definitions.HasGold, new ValueParameter(true)));
        }

        /// <summary>
        /// Create a clone of the action
        /// </summary>
        /// <returns></returns>
        public override STRIPS.Action Clone() => new SellGoldAction();

        /// <summary>
        /// Execution of the action by the agent
        /// </summary>
        /// <param name="agent">The agent who executes the action</param>
        /// <returns>An asynchronous task in which the action is performed</returns>
        public override async Task Execute(GoldMineAgent agent)
        {
            await Task.Delay(2000);
            agent.HasGold = false;
            Console.WriteLine(ToString());
        }

        /// <summary>
        /// Override of the toString method for proper reporting
        /// </summary>
        /// <returns>A string representation of this action</returns>
        public override string ToString() => "SellGold(" + Assignments["param1"] + ", "
            + Assignments["param2"] + ")";
    }
}