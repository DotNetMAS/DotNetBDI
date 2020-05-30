using NetBDI.STRIPS.UnitTests.Action;
using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Plan
{
    public class WhenCreated
    {
        private STRIPS.Plan<SomeAction> _result;

        [SetUp]
        public void Setup()
        {
            _result = new STRIPS.Plan<SomeAction>();
        }

        [Test]
        public void ThenGoalsStepsAreEmptyButNotNull()
        {
            Assert.IsNotNull(_result.Steps);
            Assert.IsEmpty(_result.Steps);
        }
    }
}