using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.ComplexGoal
{
    public class WhenCreated
    {
        private STRIPS.ComplexGoal _result;

        [SetUp]
        public void Setup()
        {
            _result = new STRIPS.ComplexGoal();
        }

        [Test]
        public void ThenGoalsAreEmptyButNotNull()
        {
            Assert.IsNotNull(_result.Goals);
            Assert.IsEmpty(_result.Goals);
        }
    }
}