using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class WhenIsApplicableForAddCalledWithGoalThatMatchesAndConstraintNotViolated
    {
        private STRIPS.Fact _result;
        private STRIPS.Fact _fact;
        private readonly SomeOtherAction _action = new SomeOtherAction();

        [SetUp]
        public void Setup()
        {
            _fact = new STRIPS.Fact("OneParam", new STRIPS.ValueParameter("test"));
            _result = _action.IsApplicableForAdd(new STRIPS.SimpleGoal(_fact), new List<STRIPS.Fact>());
        }

        [Test]
        public void ThenReturnsMoreGenericVersionOfOriginalFactFromTheAction()
        {
            Assert.AreEqual(_action.AddList[1], _result);
        }
    }
}