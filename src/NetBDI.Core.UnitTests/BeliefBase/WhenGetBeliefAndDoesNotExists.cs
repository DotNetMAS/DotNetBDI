using NUnit.Framework;

namespace NetBDI.Core.UnitTests.BeliefBase
{
    public class WhenGetBeliefAndDoesNotExists
    {
        private Core.Belief _result;

        [SetUp]
        public void Setup()
        {
            var beliefBase = new Core.BeliefBase();
            _result = beliefBase.GetBelief("name");
        }

        [Test]
        public void ThenResultIsNull()
        {
            Assert.IsNull(_result);
        }
    }
}