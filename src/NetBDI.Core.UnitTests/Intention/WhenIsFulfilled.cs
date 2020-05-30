using Moq;
using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Intention
{
    public class WhenIsFulfilled
    {
        private bool _result;
        private Mock<ADesire> _desire;

        [SetUp]
        public void Setup()
        {
            _desire = new Mock<ADesire>();
            _desire.Setup(x => x.IsFulfilled()).Returns(true);
            var intent = new Intention<AnAction, AnAgent, AnEnvironment>(_desire.Object);
            _result = intent.IsFulfilled();
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.IsTrue(_result);
        }

        [Test]
        public void ThenIsFulfilledIsCalledOnDesire()
        {
            _desire.Verify(x => x.IsFulfilled(), Times.Once);
        }
    }
}