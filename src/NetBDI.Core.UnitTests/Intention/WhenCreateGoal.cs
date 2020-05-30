using Moq;
using NetBDI.STRIPS;
using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Intention
{
    public class WhenCreateGoal
    {
        private ComplexGoal _result;
        private Mock<ADesire> _desire;
        private readonly ComplexGoal _goal = new ComplexGoal();

        [SetUp]
        public void Setup()
        {
            _desire = new Mock<ADesire>();
            _desire.Setup(x => x.CreateGoal()).Returns(_goal);
            var intent = new Intention<AnAction, AnAgent, AnEnvironment>(_desire.Object);
            _result = intent.CreateGoal();
        }

        [Test]
        public void ThenResultIsComplexGoal()
        {
            Assert.AreEqual(_goal, _result);
        }

        [Test]
        public void ThenCreateGoalIsCalledOnDesire()
        {
            _desire.Verify(x => x.CreateGoal(), Times.Once);
        }
    }
}