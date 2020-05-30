using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.SimpleGoal
{
    public class WhenIsFulFulledIsCalledAndNoMatchingFacWithSameName
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var goal  = new STRIPS.SimpleGoal(new STRIPS.Fact("Test", new STRIPS.ValueParameter("test")));
            _result = goal.IsFulFilled(new List<STRIPS.Fact> { new STRIPS.Fact("blurp")});
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.False(_result);
        }
    }
}