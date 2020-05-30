using NetBDI.STRIPS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetBDI.Core.UnitTests
{
    public class AnAgent : Agent<AnAction, AnAgent, AnEnvironment>
    {
        public AnAgent(AnEnvironment environment, CommitmentType? comType = null) : base(environment, comType) { }

        protected override Task Execute(AnAction action) => Task.CompletedTask;

        protected override List<Fact> ExtractFactsFromBeliefBase() => new List<Fact>();

        protected override Dictionary<string, object> See() => new Dictionary<string, object>();

        protected override void UpdateBeliefs(Dictionary<string, object> percepts) { }
    }
}