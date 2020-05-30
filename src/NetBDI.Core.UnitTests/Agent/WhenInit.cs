using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.Core.UnitTests.Agent
{
    public class WhenInit
    {
        private readonly ADesire _desire = new ADesire();

        [SetUp]
        public void Setup()
        {
            var agent = new AnAgent(new AnEnvironment());
            agent.Init(new List<Desire<AnAction, AnAgent, AnEnvironment>> { _desire }, 
                new List<Intention<AnAction, AnAgent, AnEnvironment>> { new Intention<AnAction, AnAgent, AnEnvironment>(_desire) });
            agent.Stop();
        }

        [Test]
        public void ThenWeCantTestThis()
        {
            Assert.IsTrue(true);
        }
    }
}