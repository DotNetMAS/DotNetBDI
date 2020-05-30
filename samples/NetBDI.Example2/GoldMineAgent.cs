using NetBDI.Core;
using NetBDI.STRIPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBDI.Example2
{
    /// <summary>
    /// The agent for this environment. A miner who wanders, digs for gold and sells the gold in town
    /// </summary>
    public class GoldMineAgent : Agent<GoldMineAction, GoldMineAgent, GoldMineEnvironment>
    {
        /// <summary>
        /// Flag if the agent is carrying gold
        /// </summary>
        public bool HasGold { get; set; }
        /// <summary>
        /// The current x position of the agent
        /// </summary>
        public double CurrentX { get; set; }
        /// <summary>
        /// The current y position of the agent
        /// </summary>
        public double CurrentY { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="environment">The environment of the agent</param>
        /// <param name="currentX">The current x position</param>
        /// <param name="currentY">The current y position</param>
        /// <param name="comType">The commitmenttype of the agent</param>
        public GoldMineAgent(GoldMineEnvironment environment, int currentX, int currentY, CommitmentType? comType = null) : 
            base(environment, comType)
        {
            CurrentX = currentX;
            CurrentY = currentY;
        }

        /// <summary>
        /// We initialize the agent with a current set of intentions
        /// </summary>
        /// <param name="desires">The current set of desires</param>
        /// <param name="intention">The current intention</param>
        /// <returns>An asynchronous task of the agent fulfilling the intentions</returns>
        public override Task Init(List<Desire<GoldMineAction, GoldMineAgent, GoldMineEnvironment>> desires,
            List<Intention<GoldMineAction, GoldMineAgent, GoldMineEnvironment>> intentions)
        {
            BeliefBase.AddBeliefSet(new BeliefSet("mines"));
            BeliefBase.AddBeliefSet(new BeliefSet("towns"));
            Actions.Add(new GoToAction());
            Actions.Add(new MineGoldAction());
            Actions.Add(new SellGoldAction());
            return base.Init(desires, intentions);
        }

        /// <summary>
        /// We execute an action
        /// </summary>
        /// <param name="action">The action to execute</param>
        /// <returns>The asynchronous task in which the action is being performed</returns>
        protected override Task Execute(GoldMineAction action) => action.Execute(this);

        /// <summary>
        /// Extracts facts from a beliefbase
        /// </summary>
        /// <returns>A list of facts</returns>
        protected override List<Fact> ExtractFactsFromBeliefBase()
        {
            var lstFact = new List<Fact>();
            lstFact.Add(new Fact(Definitions.HasGold, new ValueParameter(BeliefBase.GetBelief("hasgold").GetValue<bool>())));

            lstFact.Add(new Fact(Definitions.In, new ValueParameter(BeliefBase.GetBelief("inx").Value), 
                new ValueParameter(BeliefBase.GetBelief("iny").Value)));

            foreach (Town town in BeliefBase.GetBeliefSet("towns").Values)
                lstFact.Add(new Fact(Definitions.Town, new ValueParameter(town.X), new ValueParameter(town.Y)));

            foreach (GoldMine mine in BeliefBase.GetBeliefSet("mines").Values)
                lstFact.Add(new Fact(Definitions.Mine, new ValueParameter(mine.X), new ValueParameter(mine.Y)));

            return lstFact;
        }

        /// <summary>
        /// Retrieval of percepts from the environment
        /// </summary>
        /// <returns>The percepts in a dictionary</returns>
        protected override Dictionary<string, object> See()
        {
            var dict = new Dictionary<string, object>();
            var lstTowns = new List<Town>();
            dict.Add("towns", lstTowns);
            foreach(var town in Environment.Towns)
                if (Math.Pow(CurrentX - town.X, 2) + Math.Pow(CurrentY - town.Y, 2) < 4)
                    lstTowns.Add(town);

            var lstMines = new List<GoldMine>();
            dict.Add("mines", lstMines);
            foreach (var mine in Environment.GoldMines)
                if (Math.Pow(CurrentX - mine.X, 2) + Math.Pow(CurrentY - mine.Y, 2) < 4)
                    lstMines.Add(mine);

            return dict;
        }

        /// <summary>
        /// We update the beliefbase
        /// </summary>
        /// <param name="percepts">The percepts we retrieved for this agent from the environment</param>
        protected override void UpdateBeliefs(Dictionary<string, object> percepts)
        {
            var beliefSetTowns = BeliefBase.GetBeliefSet("towns").Values;
            var beliefSetMines = BeliefBase.GetBeliefSet("mines").Values;
            
            foreach(var town in (percepts["towns"] as List<Town>))
                if (!beliefSetTowns.Contains(town))
                    beliefSetTowns.Add(town);

            foreach (var goldMine in beliefSetMines.Select(x => (GoldMine) x).ToList())
                if (!(percepts["mines"] as List<GoldMine>).Contains(goldMine) && 
                    Math.Pow(CurrentX - goldMine.X, 2) + Math.Pow(CurrentY - goldMine.Y, 2) < 4)
                    beliefSetMines.Remove(goldMine);

            foreach (var goldMine in (percepts["mines"] as List<GoldMine>))
                if (!beliefSetMines.Contains(goldMine))
                    beliefSetMines.Add(goldMine);

            BeliefBase.UpdateBelief(new Belief("inx", CurrentX));
            BeliefBase.UpdateBelief(new Belief("iny", CurrentY));
            BeliefBase.UpdateBelief(new Belief("hasgold", HasGold));
        }
    }
}