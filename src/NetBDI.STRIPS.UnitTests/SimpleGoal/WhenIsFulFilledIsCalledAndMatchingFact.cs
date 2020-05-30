using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.SimpleGoal
{
    public class WhenIsFulFulledIsCalledAndMatchingFact
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var goal  = new STRIPS.SimpleGoal(new STRIPS.Fact("Test", new STRIPS.ValueParameter("test")));
            _result = goal.IsFulFilled(new List<STRIPS.Fact> { new STRIPS.Fact("Test", new STRIPS.ValueParameter("test"))});
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.True(_result);
        }
    }
}