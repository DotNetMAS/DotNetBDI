using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class WhenIsApplicableForAddCalledWithGoalThatDoesNotMatch
    {
        private STRIPS.Fact _result;

        [SetUp]
        public void Setup()
        {
            var action = new SomeAction();
            _result = action.IsApplicableForAdd(new STRIPS.SimpleGoal(new STRIPS.Fact("OneParam")), new List<STRIPS.Fact>());
        }

        [Test]
        public void ThenReturnsNull()
        {
            Assert.IsNull(_result);
        }
    }
}