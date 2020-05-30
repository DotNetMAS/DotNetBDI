using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Desire
{
    public class WhenIsFulfilled
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var desire = new ADesire
            {
                TargetCondition = () => true
            };
            _result = desire.IsFulfilled();
        }

        [Test]
        public void ThenResultIsResultOfTargetCondition()
        {
            Assert.IsTrue(_result);
        }
    }
}