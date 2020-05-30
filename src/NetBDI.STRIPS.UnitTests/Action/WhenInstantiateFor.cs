using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class WhenInstantiateFor
    {
        private STRIPS.Action _result;
        private Mock<SomeAction> _mockAction;
        private STRIPS.SimpleGoal _goal;
        private readonly STRIPS.Fact _fact = new STRIPS.Fact("on", new STRIPS.NamedParameter("param1"));
        private readonly object _obj = new object();
        private readonly List<STRIPS.Fact> _currentBeliefs = new List<STRIPS.Fact>();

        [SetUp]
        public void Setup()
        {
            _mockAction = new Mock<SomeAction>();
            _mockAction.Setup(x => x.Clone()).Returns(new SomeAction());
            _goal = new STRIPS.SimpleGoal(new STRIPS.Fact("OneParam", new STRIPS.ValueParameter(_obj)));
            _mockAction.Setup(x => x.GetAssignment(It.IsAny<STRIPS.Fact>(), It.IsAny<STRIPS.SimpleGoal>(), _currentBeliefs)).Returns(new Dictionary<string, object> { { "test", _obj } });
            _mockAction.Setup(x => x.IsApplicableForAdd(_goal, _currentBeliefs)).Returns(_fact);    
            _result = _mockAction.Object.InstantiateFor(_goal, _currentBeliefs);
        }

        [Test]
        public void ThenCloneIsCalled()
        {
            _mockAction.Verify(x => x.Clone(), Times.Once);
        }

        [Test]
        public void ThenAssignmentIsCreatedForAddFactAndAppliedToAllFacts()
        {
            var valParam = _result.AddList[1].Parameters[0] as STRIPS.ValueParameter;
            Assert.AreEqual(_obj, valParam.Value);
        }
    }
}