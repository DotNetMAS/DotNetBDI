using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.ComplexGoal
{
    public class WhenIsFulFilledIsCalledAndOneSubGoalNotFulFilled
    {
        private bool _result;
        private Mock<STRIPS.SimpleGoal> _subGoal1;
        private Mock<STRIPS.SimpleGoal> _subGoal2;
        private Mock<STRIPS.SimpleGoal> _subGoal3;
        private readonly List<STRIPS.Fact> _facts = new List<STRIPS.Fact>();

        [SetUp]
        public void Setup()
        {
            _subGoal3 = new Mock<STRIPS.SimpleGoal>(new STRIPS.Fact("name"));
            _subGoal3.Setup(x => x.IsFulFilled(It.IsAny<List<STRIPS.Fact>>())).Returns(true);
            _subGoal1 = new Mock<STRIPS.SimpleGoal>(new STRIPS.Fact("name"));
            _subGoal1.Setup(x => x.IsFulFilled(It.IsAny<List<STRIPS.Fact>>())).Returns(false);
            _subGoal2 = new Mock<STRIPS.SimpleGoal>(new STRIPS.Fact("name"));
            _subGoal2.Setup(x => x.IsFulFilled(It.IsAny<List<STRIPS.Fact>>())).Returns(true);
            var goal = new STRIPS.ComplexGoal();
            goal.Goals.Add(_subGoal3.Object);
            goal.Goals.Add(_subGoal1.Object);
            goal.Goals.Add(_subGoal2.Object);
            _result = goal.IsFulFilled(_facts);
        }

        [Test]
        public void ThenReturnsFalse()
        {
            Assert.False(_result);
        }

        [Test]
        public void ThenIsFulFilledIsCalledOnlyOnSubGoalsUntilFailed()
        {
            _subGoal3.Verify(x => x.IsFulFilled(_facts), Times.Once);
            _subGoal1.Verify(x => x.IsFulFilled(_facts), Times.Once);
            _subGoal2.Verify(x => x.IsFulFilled(_facts), Times.Never);
        }
    }
}