using Moq;
using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Intention
{
    public class WhenIsAchievable
    {
        private bool _result;
        private Mock<ADesire> _desire;

        [SetUp]
        public void Setup()
        {
            _desire = new Mock<ADesire>();
            _desire.Setup(x => x.IsAchievable()).Returns(true);
            var intent = new Intention<AnAction, AnAgent, AnEnvironment>(_desire.Object);
            _result = intent.IsAchievable();
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.IsTrue(_result);
        }

        [Test]
        public void ThenIsAchievableIsCalledOnDesire()
        {
            _desire.Verify(x => x.IsAchievable(), Times.Once);
        }
    }
}