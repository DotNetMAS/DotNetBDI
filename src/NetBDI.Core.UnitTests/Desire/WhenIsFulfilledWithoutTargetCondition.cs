using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Desire
{
    public class WhenIsFulfilledWithoutTargetCondition
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var desire = new ADesire();
            _result = desire.IsFulfilled();
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.IsFalse(_result);
        }
    }
}