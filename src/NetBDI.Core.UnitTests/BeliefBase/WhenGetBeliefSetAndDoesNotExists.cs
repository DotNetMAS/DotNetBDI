using NUnit.Framework;

namespace NetBDI.Core.UnitTests.BeliefBase
{
    public class WhenGetBeliefSetAndDoesNotExists
    {
        private Core.BeliefSet _result;

        [SetUp]
        public void Setup()
        {
            var beliefBase = new Core.BeliefBase();
            _result = beliefBase.GetBeliefSet("name");
        }

        [Test]
        public void ThenResultIsNull()
        {
            Assert.IsNull(_result);
        }
    }
}