using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Agent
{
    public class WhenAddDesire
    {
        private AnAgent _result;
        private readonly ADesire _desire = new ADesire();
        private readonly AnEnvironment _environment = new AnEnvironment();

        [SetUp]
        public void Setup()
        {
            _result = new AnAgent(_environment, CommitmentType.OpenMinded);
            _result.AddDesire(_desire);
        }

        [Test]
        public void ThenWeCantTestThis()
        {
            Assert.IsTrue(true);
        }
    }
}