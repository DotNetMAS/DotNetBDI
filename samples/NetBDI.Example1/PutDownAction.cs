using NetBDI.STRIPS;
using System;
using System.Threading.Tasks;

namespace NetBDI.Example1
{
    /// <summary>
    /// An action that simulates a robot holding a block and putting it down on the table
    /// </summary>
    public class PutDownAction : BlocksworldAction
    {
        private readonly Table _table;

        /// <summary>
        /// Default constructor that initializes preconditions, add/delete list and constraints
        /// </summary>
        /// <param name="table">The table of our problem</param>
        public PutDownAction(Table table)
        {
            _table = table;
            var block = new NamedParameter("param1");
            var goal1 = new Fact(Definitions.ArmHolds, block);
            Preconditions.Add(goal1);
            AddList.Add(new Fact(Definitions.ArmHolds, new ValueParameter(null)));
            AddList.Add(new Fact(Definitions.On, block, new ValueParameter(table)));
            AddList.Add(new Fact(Definitions.Clear, block));
            DeleteList.Add(goal1);
            Constraints.Add(TypeCheck(typeof(Block), block));
        }

        /// <summary>
        /// Clone the action for instantiation
        /// </summary>
        /// <returns>A new action of this type</returns>
        public override STRIPS.Action Clone() => new PutDownAction(_table);

        /// <summary>
        /// Override of the toString method for proper reporting
        /// </summary>
        /// <returns>A string representation of this action</returns>
        public override string ToString() => "PutDown(" + Assignments["param1"].ToString() + ", table)";

        /// <summary>
        /// Execute this action by the agent on the environment
        /// </summary>
        /// <param name="agent">The agent who will execute the action</param>
        /// <returns>An asynchronous task being performed</returns>
        public override async Task Execute(BlocksworldAgent agent)
        {
            await Task.Delay(1000);
            agent.Environment.Table.AddBlock(agent.InArm);
            agent.InArm.Lower = agent.Environment.Table;
            agent.InArm = null;
            Console.WriteLine(ToString());
        }
    }
}