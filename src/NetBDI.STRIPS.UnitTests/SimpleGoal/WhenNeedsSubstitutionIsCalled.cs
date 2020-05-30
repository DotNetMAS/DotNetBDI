using Moq;
using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.SimpleGoal
{
    class WhenNeedsSubstitutionIsCalled
    {
        private bool _result;
        private Mock<STRIPS.Fact> _fact;

        [SetUp]
        public void Setup()
        {
            _fact = new Mock<STRIPS.Fact>("test");
            _fact.Setup(x => x.NeedsSubstitution()).Returns(true);
            var goal = new STRIPS.SimpleGoal(_fact.Object);
            _result = goal.NeedsSubstitution();
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.True(_result);
        }

        [Test]
        public void ThenNeedsSubstitutionIsCalledOnFact()
        {
            _fact.Verify(x => x.NeedsSubstitution(), Times.Once);
        }
    }
}