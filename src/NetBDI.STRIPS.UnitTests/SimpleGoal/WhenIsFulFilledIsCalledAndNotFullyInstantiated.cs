using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.SimpleGoal
{
    public class WhenIsFulFulledIsCalledAndNotFullyInstantiated
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var goal  = new STRIPS.SimpleGoal(new STRIPS.Fact("Test", new STRIPS.NamedParameter("test")));
            _result = goal.IsFulFilled(new List<STRIPS.Fact>());
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.False(_result);
        }
    }
}