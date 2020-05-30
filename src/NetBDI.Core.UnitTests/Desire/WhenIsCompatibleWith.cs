using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Desire
{
    public class WhenIsCompatibleWith
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var desire = new ADesire
            {
                TargetCondition = () => true
            };
            _result = desire.IsCompatibleWith(desire);
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.IsFalse(_result);
        }
    }
}