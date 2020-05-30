using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Desire
{
    public class WhenIsAchievable
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var desire = new ADesire
            {
                Precondition = () => false
            };
            _result = desire.IsAchievable();
        }

        [Test]
        public void ThenResultIsResultOfPrecondition()
        {
            Assert.IsFalse(_result);
        }
    }
}