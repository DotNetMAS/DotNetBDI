using NetBDI.STRIPS;
using System;
using System.Threading.Tasks;

namespace NetBDI.Example1
{
    /// <summary>
    /// An action that simulates a robot picking a block from the table and holding it in its arm
    /// </summary>
    public class PickupAction : BlocksworldAction
    {
        private readonly Table _table;

        /// <summary>
        /// Default constructor that initializes preconditions, add/delete list and constraints
        /// </summary>
        /// <param name="table">The table of our problem</param>
        public PickupAction(Table table)
        {
            _table = table;
            var block = new NamedParameter("param1");
            Preconditions.Add(new Fact(Definitions.ArmHolds, new ValueParameter(null)));
            Preconditions.Add(new Fact(Definitions.On, block, new ValueParameter(table)));
            Preconditions.Add(new Fact(Definitions.Clear, block));
            AddList.Add(new Fact(Definitions.ArmHolds, block));
            DeleteList.Add(new Fact(Definitions.On, block, new ValueParameter(table)));
            DeleteList.Add(new Fact(Definitions.ArmHolds, new ValueParameter(null)));
            DeleteList.Add(new Fact(Definitions.Clear, block));
            Constraints.Add(TypeCheck(typeof(Block), block));
        }

        /// <summary>
        /// Clone the action for instantiation
        /// </summary>
        /// <returns>A new action of this type</returns>
        public override STRIPS.Action Clone() => new PickupAction(_table);

        /// <summary>
        /// Override of the toString method for proper reporting
        /// </summary>
        /// <returns>A string representation of this action</returns>
        public override string ToString() => "PickUp(" + Assignments["param1"].ToString() + ", table)";

        /// <summary>
        /// Execute this action by the agent on the environment
        /// </summary>
        /// <param name="agent">The agent who will execute the action</param>
        /// <returns>An asynchronous task being performed</returns>
        public override async Task Execute(BlocksworldAgent agent)
        {
            await Task.Delay(1000);
            var block = (Block)Assignments["param1"];
            var tableBlock = agent.Environment.FindBlock(block);
            agent.InArm = tableBlock;           
            tableBlock.Lower.RemoveBlock(tableBlock);
            tableBlock.Lower = null;
            Console.WriteLine(ToString());
        }
    }
}