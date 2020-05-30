using Moq;
using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.ComplexGoal
{
    public class WhenCallingIsSameAndGoalFactsAreSame
    {
        private bool _result;
        private Mock<STRIPS.Fact> _subFact1;
        private Mock<STRIPS.Fact> _subFact2;

        [SetUp]
        public void Setup()
        {
            _subFact1 = new Mock<STRIPS.Fact>("name");
            _subFact1.Setup(x => x.IsSameAs(It.IsAny<STRIPS.Fact>())).Returns(true);
            _subFact2 = new Mock<STRIPS.Fact>("name2");
            _subFact2.Setup(x => x.IsSameAs(It.IsAny<STRIPS.Fact>())).Returns(true);

            var subGoal1 = new STRIPS.SimpleGoal(_subFact1.Object);
            var subGoal2 = new STRIPS.SimpleGoal(_subFact2.Object);
            var complex1 = new STRIPS.ComplexGoal();
            complex1.Goals.Add(subGoal1);
            complex1.Goals.Add(subGoal2);
            var complex2 = new STRIPS.ComplexGoal();
            complex2.Goals.Add(subGoal1);
            complex2.Goals.Add(subGoal2);
            _result = complex1.IsSame(complex2);
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.True(_result);
        }

        [Test]
        public void ThenIsSameAsIsCalledOnFacts()
        {
            _subFact1.Verify(x => x.IsSameAs(_subFact1.Object), Times.Once);
            _subFact2.Verify(x => x.IsSameAs(_subFact2.Object), Times.Once);
        }
    }
}