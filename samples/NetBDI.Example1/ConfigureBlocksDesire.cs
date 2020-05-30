using NetBDI.Core;
using NetBDI.STRIPS;
using System.Collections.Generic;
using System.Linq;

namespace NetBDI.Example1
{
    /// <summary>
    /// The intention of an agent to configure the blocks in a certain configuration
    /// </summary>
    public class ConfigureBlocksDesire : Desire<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>
    {
        /// <summary>
        /// Does the agent need to be stopped
        /// </summary>
        private bool _stop;

        /// <summary>
        /// The target configuration as displayed on a table
        /// </summary>
        public Table Target { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="target">The target configuration of this intention</param>
        /// <param name="strength">The strength of the desire</param>
        public ConfigureBlocksDesire(Table target, int strength) : base(strength)
        {
            TargetCondition = IsTargetAchieved;
            Precondition = AreAllBlocksAvailable;
            Target = target;
        }

        /// <summary>
        /// We stop the agent
        /// </summary>
        public void Stop() => _stop = true;

        /// <summary>
        /// From a desire, we create a complex goal
        /// </summary>
        /// <returns>A complex goal to be used in STRIPS</returns>
        public override ComplexGoal CreateGoal()
        {
            var complexGoal = new ComplexGoal();
            var stacks = Target.GetStacks();
            foreach (var stack in stacks)
            {
                stack.Reverse();
                foreach (var block in stack)
                {
                    if (block == stack.First())
                        complexGoal.Goals.Add(new SimpleGoal(new Fact(Definitions.Clear, new ValueParameter(block))));
                    
                    complexGoal.Goals.Add(new SimpleGoal(new STRIPS.Fact(Definitions.On, new ValueParameter(block), new ValueParameter(block.Lower))));
                }
            }
            return complexGoal;
        }

        /// <summary>
        /// Test to see if the target is achieved by an agent for a desire
        /// </summary>
        /// <returns>True if we have the target configuration on the table, false otherwise</returns>
        private bool IsTargetAchieved()
        {
            if (_stop)
                return true;

            var stacksAgent = Agent.Environment.Table.GetStacks();
            var stacksTarget = Target.GetStacks();

            foreach (var stackTarget in stacksTarget)
            {
                var found = false;
                for (var j = 0; j < stacksAgent.Count() && !found; j++)
                {
                    var stackAgent = stacksAgent[j];
                    if (stackTarget.Count() != stackAgent.Count())
                        return false;

                    var same = true;
                    for (var i = 0; i < stackAgent.Count() && same; i++)
                        if (!stackAgent[i].Equals(stackTarget[i]))
                            same = false;

                    if (same)
                        found = true;
                }
                if (!found)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Are all blocks available in the current beliefset of the agent
        /// </summary>
        /// <returns>True if all blocks of the targetconfiguration are available on the table, false otherwise</returns>
        private bool AreAllBlocksAvailable()
        {
            var targetBlocks = Target.Blocks.SelectMany(x => x.GetUpperStack());
            var currentBlocks = Agent.BeliefBase.GetBeliefSet("blocks").Values;
            if (Agent.BeliefBase.GetBelief("inarm").Value != null)
                currentBlocks.Add(Agent.BeliefBase.GetBelief("inarm").Value);

            foreach (var targetBlock in targetBlocks)
                if (!currentBlocks.Any(x => ((Block)x).Color.Equals(targetBlock.Color)))
                    return false;

            return true;
        }
    }
}