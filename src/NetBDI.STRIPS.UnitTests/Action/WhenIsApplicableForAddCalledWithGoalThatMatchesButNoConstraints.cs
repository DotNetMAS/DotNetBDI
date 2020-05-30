using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class WhenIsApplicableForAddCalledWithGoalThatMatchesButNoConstraints
    {
        private STRIPS.Fact _result;
        private STRIPS.Fact _fact;
        private readonly SomeAction _action = new SomeAction();

        [SetUp]
        public void Setup()
        {
            _fact = new STRIPS.Fact("OneParam", new STRIPS.ValueParameter(null));
            _result = _action.IsApplicableForAdd(new STRIPS.SimpleGoal(_fact), new List<STRIPS.Fact>());
        }

        [Test]
        public void ThenReturnsFirstResult()
        {
            Assert.AreEqual(_action.AddList[1], _result);
        }
    }
}