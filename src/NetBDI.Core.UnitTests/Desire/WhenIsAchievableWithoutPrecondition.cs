using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Desire
{
    public class WhenIsAchievableWithoutPrecondition
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var desire = new ADesire();
            _result = desire.IsAchievable();
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.IsTrue(_result);
        }
    }
}