using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class WhenIsApplicableForAddCalledWithGoalThatMatchesButConstraintViolated
    {
        private STRIPS.Fact _result;
        private STRIPS.Fact _fact;

        [SetUp]
        public void Setup()
        {
            _fact = new STRIPS.Fact("OneParam", new STRIPS.ValueParameter(null));
            var action = new SomeOtherAction();
            _result = action.IsApplicableForAdd(new STRIPS.SimpleGoal(_fact), new List<STRIPS.Fact>());
        }

        [Test]
        public void ThenReturnsNull()
        {
            Assert.IsNull(_result);
        }
    }
}