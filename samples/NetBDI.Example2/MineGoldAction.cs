using NetBDI.STRIPS;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetBDI.Example2
{
    /// <summary>
    /// An action to mine gold
    /// </summary>
    public class MineGoldAction : GoldMineAction
    {
        /// <summary>
        /// Default constructor that initializes preconditions, add/delete list and constraints
        /// </summary>
        public MineGoldAction()
        {
            var x = new NamedParameter("param1");
            var y = new NamedParameter("param2");
            Preconditions.Add(new Fact(Definitions.HasGold, new ValueParameter(false)));
            Preconditions.Add(new Fact(Definitions.Mine, x, y));
            Preconditions.Add(new Fact(Definitions.In, x, y));
            AddList.Add(new Fact(Definitions.HasGold, new ValueParameter(true)));
            DeleteList.Add(new Fact(Definitions.Mine, x, y));
            DeleteList.Add(new Fact(Definitions.HasGold, new ValueParameter(false)));
        }

        /// <summary>
        /// Create clone of the action
        /// </summary>
        /// <returns>The clone of the action</returns>
        public override STRIPS.Action Clone() => new MineGoldAction();

        /// <summary>
        /// Execution of the action by an agent
        /// </summary>
        /// <param name="agent">The agent who executes the action</param>
        /// <returns>An asynchronous task in which the action is performed</returns>
        public override async Task Execute(GoldMineAgent agent)
        {
            await Task.Delay(5000);
            var toRemove = agent.Environment.GoldMines.First(x => x.X == agent.CurrentX && x.Y == agent.CurrentY);
            agent.Environment.GoldMines.Remove(toRemove);
            agent.HasGold = true;
            Console.WriteLine(ToString());
        }

        /// <summary>
        /// Override of the toString method for proper reporting
        /// </summary>
        /// <returns>A string representation of this action</returns>
        public override string ToString() => "MineGold(" + Assignments["param1"] + ", "
            + Assignments["param2"] + ")";
    }
}