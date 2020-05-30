using NUnit.Framework;

namespace NetBDI.Core.UnitTests.BeliefBase
{
    public class WhenUpdateBeliefSetAndOld
    {
        private Core.BeliefBase _result;
        private readonly Core.BeliefSet _oldSet = new Core.BeliefSet("name");
        private readonly Core.BeliefSet _set = new Core.BeliefSet("name");

        [SetUp]
        public void Setup()
        {
            _result = new Core.BeliefBase();
            _result.AddBeliefSet(_oldSet);
            _result.UpdateBeliefSet(_set);
        }

        [Test]
        public void ThenBeliefSetIsUpdatedAndRetrievable()
        {
            Assert.AreEqual(_set, _result.GetBeliefSet("name"));
        }
    }
}