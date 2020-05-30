using NetBDI.Core;
using NetBDI.STRIPS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBDI.Example1
{
    /// <summary>
    /// The agent residing in the environment and performing actions for this problem
    /// </summary>
    public class BlocksworldAgent : Agent<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>
    {
        /// <summary>
        /// An agent has an arm that can contain a block
        /// </summary>
        public Block InArm { get; set; } = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="environment">The environment in which the agent operates</param>
        public BlocksworldAgent(BlocksworldEnvironment environment, CommitmentType? comType = null) : base(environment, comType) { }

        /// <summary>
        /// We initialize the agent with a current set of intentions
        /// </summary>
        /// <param name="desires">The current set of desires</param>
        /// <param name="intentions">The current intentions</param>
        /// <returns>An asynchronous task of the agent fulfilling the intentions</returns>
        public override Task Init(List<Desire<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>> desires, 
            List<Intention<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>> intentions)
        {
            var table = new Table();
            BeliefBase.AddBeliefSet(new BeliefSet("blocks"));
            BeliefBase.UpdateBelief(new Belief("inarm", null));
            BeliefBase.UpdateBelief(new Belief("table", table));
            Actions.Add(new PickupAction(table));
            Actions.Add(new PutDownAction(table));
            Actions.Add(new StackAction());
            Actions.Add(new UnstackAction());
            return base.Init(desires, intentions);
        }

        /// <summary>
        /// Extracts facts for STRIPS from the current beliefbase
        /// </summary>
        /// <returns>A list of STRIP facts</returns>
        protected override List<Fact> ExtractFactsFromBeliefBase()
        {
            var facts = new List<Fact> { new Fact("ArmHolds", new ValueParameter(BeliefBase.GetBelief("inarm").Value)) };
            var blocks = BeliefBase.GetBeliefSet("blocks").Values;
            foreach(Block block in blocks)
            {
                if(block.Lower != null)
                    facts.Add(new Fact("On", new ValueParameter(block), new ValueParameter(block.Lower)));

                if (block.Upper == null)
                    facts.Add(new Fact("Clear", new ValueParameter(block)));
            }
            return facts;
        }

        /// <summary>
        /// An action performed by the agent to see the environment
        /// </summary>
        /// <returns>The percepts as a list of keyvalue pairs where each perceived object is linked to a name</returns>
        protected override Dictionary<string, object> See()
        {
            //the agent perfectly sees the entire table in the blink of an eye
            return new Dictionary<string, object> {{ "table", Environment.Table }};
        }

        /// <summary>
        /// The beliefbase of the agent is updated through the received percepts
        /// </summary>
        /// <param name="percepts">The percepts as a list of keyvalue pairs</param>
        protected override void UpdateBeliefs(Dictionary<string, object> percepts)
        {
            var perceptedTable = (Table)percepts["table"];
            BeliefBase.UpdateBelief(new Belief("table", perceptedTable));
            BeliefBase.UpdateBelief(new Belief("inarm", InArm));
            var beliefsetBlocks = BeliefBase.GetBeliefSet("blocks");
            var newBeliefSetBlocks = new BeliefSet("blocks");
            foreach (var block in perceptedTable.Blocks)
                UpdateFactForOneBlock(beliefsetBlocks, newBeliefSetBlocks, block);

            BeliefBase.UpdateBeliefSet(newBeliefSetBlocks);
        }

        /// <summary>
        /// Execute an action
        /// </summary>
        /// <param name="action">The action to execute</param>
        /// <returns>An asynchronous task performing the action</returns>
        protected override Task Execute(BlocksworldAction action) => action.Execute(this);

        /// <summary>
        /// Does our current intention needs reconsideration after we updated our beliefs
        /// </summary>
        /// <returns>True if we need to reconsider the current intention, false otherwise</returns>
        protected override bool NeedsReconsideration()
        {
            var currentStrength = CurrentIntentions.First().GetStrength();
            return Desires.Any(x => x.Strength >= currentStrength * 2);
        }

        /// <summary>
        /// A recursive function that will update the beliefs for one block
        /// </summary>
        /// <param name="oldBeliefsetBlocks">The old beliefbase</param>
        /// <param name="newBeliefsetBlocks">The new beliefbase</param>
        /// <param name="block">The block being perceived</param>
        private void UpdateFactForOneBlock(BeliefSet oldBeliefsetBlocks, BeliefSet newBeliefsetBlocks, Block block)
        {
            newBeliefsetBlocks.AddFact(block);
            if (block.Upper != null)
                UpdateFactForOneBlock(oldBeliefsetBlocks, newBeliefsetBlocks, block.Upper);
        }
    }
}