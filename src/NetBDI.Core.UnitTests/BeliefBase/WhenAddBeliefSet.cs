using NUnit.Framework;

namespace NetBDI.Core.UnitTests.BeliefBase
{
    public class WhenAddBeliefSet
    {
        private Core.BeliefBase _result;
        private readonly Core.BeliefSet _set = new Core.BeliefSet("name");

        [SetUp]
        public void Setup()
        {
            _result = new Core.BeliefBase();
            _result.AddBeliefSet(_set);
        }

        [Test]
        public void ThenBeliefSetIsAddedAndRetrievable()
        {
            Assert.AreEqual(_set, _result.GetBeliefSet("name"));
        }
    }
}