using Moq;
using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Intention
{
    public class WhenIsCompatibleWith
    {
        private bool _result;
        private Mock<ADesire> _desire;

        [SetUp]
        public void Setup()
        {
            _desire = new Mock<ADesire>();
            _desire.Setup(x => x.IsFulfilled()).Returns(true);
            var intent = new Intention<AnAction, AnAgent, AnEnvironment>(_desire.Object);
            _result = intent.IsCompatibleWith(_desire.Object);
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.IsFalse(_result);
        }

        [Test]
        public void ThenIsCompatibleWithCalledOnDesire()
        {
            _desire.Verify(x => x.IsCompatibleWith(_desire.Object), Times.Once);
        }
    }
}