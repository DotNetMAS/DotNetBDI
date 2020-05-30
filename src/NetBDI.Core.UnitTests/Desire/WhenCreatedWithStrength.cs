using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Desire
{
    public class WhenCreatedWithStrength
    {
        private ADesire _result;

        [SetUp]
        public void Setup()
        {
            _result = new ADesire(5);
        }

        [Test]
        public void ThenStrengthIsSet()
        {
            Assert.AreEqual(5, _result.Strength);
        }
    }
}