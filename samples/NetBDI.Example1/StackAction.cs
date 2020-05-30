using NetBDI.STRIPS;
using System;
using System.Threading.Tasks;

namespace NetBDI.Example1
{
    /// <summary>
    /// A stack action that simulates a robot holding a block and stacks it on another block (NOT the table)
    /// </summary>
    public class StackAction : BlocksworldAction
    {
        /// <summary>
        /// Default constructor that initializes preconditions, add/delete list and constraints
        /// </summary>
        public StackAction()
        {
            var param1 = new NamedParameter("param1");
            var param2 = new NamedParameter("param2");
            var goal1 = new Fact(Definitions.ArmHolds, param1);
            var goal2 = new Fact(Definitions.Clear, param2);
            Preconditions.Add(goal1);
            Preconditions.Add(goal2);
            AddList.Add(new Fact(Definitions.ArmHolds, new ValueParameter(null)));
            AddList.Add(new Fact(Definitions.On, param1, param2));
            AddList.Add(new Fact(Definitions.Clear, param1));
            DeleteList.Add(goal1);
            DeleteList.Add(goal2);
            Constraints.Add(TypeCheck(typeof(Block), param1));
            Constraints.Add(TypeCheck(typeof(Block), param2));
            Constraints.Add(NotEqualsCheck(param1, param2));
        }

        /// <summary>
        /// Clone the action for instantiation
        /// </summary>
        /// <returns>A new action of this type</returns>
        public override STRIPS.Action Clone() => new StackAction();

        /// <summary>
        /// Override of the toString method for proper reporting
        /// </summary>
        /// <returns>A string representation of this action</returns>
        public override string ToString() => "Stack(" + Assignments["param1"].ToString() + ", " + Assignments["param2"].ToString() + ")";

        /// <summary>
        /// Execute this action by the agent on the environment
        /// </summary>
        /// <param name="agent">The agent who will execute the action</param>
        /// <returns>An asynchronous task being performed</returns>
        public override async Task Execute(BlocksworldAgent agent)
        {
            await Task.Delay(1000);
            var block = (Block)Assignments["param2"];
            var foundBlock = agent.Environment.FindBlock(block);
            foundBlock.AddBlock(agent.InArm);
            agent.InArm.Lower = foundBlock;
            agent.InArm = null;
            Console.WriteLine(ToString());
        }
    }
}