using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Desire
{
    public class WhenCreated
    {
        private ADesire _result;

        [SetUp]
        public void Setup()
        {
            _result = new ADesire();
        }

        [Test]
        public void ThenStrengthIsZero()
        {
            Assert.Zero(_result.Strength);
        }
    }
}