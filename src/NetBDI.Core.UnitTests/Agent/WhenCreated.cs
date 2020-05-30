using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Agent
{
    public class WhenCreated
    {
        private AnAgent _result;
        private readonly AnEnvironment _environment = new AnEnvironment();

        [SetUp]
        public void Setup()
        {
            _result = new AnAgent(_environment, CommitmentType.OpenMinded);
        }

        [Test]
        public void ThenEnvironmentIsSet()
        {
            Assert.AreEqual(_environment, _result.Environment);
        }

        [Test]
        public void ThenCurrentIntentionIsNotNullButEmpty()
        {
            Assert.IsNotNull(_result.CurrentIntentions);
            Assert.IsEmpty(_result.CurrentIntentions);
        }

        [Test]
        public void ThenBeliefBaseIsInitialized()
        {
            Assert.IsNotNull(_result.BeliefBase);
        }
    }
}