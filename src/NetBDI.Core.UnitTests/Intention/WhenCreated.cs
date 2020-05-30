using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Intention
{
    public class WhenCreated
    {
        private Intention<AnAction, AnAgent, AnEnvironment> _result;
        private readonly ADesire _desire = new ADesire();

        [SetUp]
        public void Setup()
        {
            _result = new Intention<AnAction, AnAgent, AnEnvironment>(_desire);
        }

        [Test]
        public void ThenDesireIsSet()
        {
            Assert.AreEqual(_result.Desire, _desire);
        }
    }
}