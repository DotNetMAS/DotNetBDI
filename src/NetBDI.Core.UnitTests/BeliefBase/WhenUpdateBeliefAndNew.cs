using NUnit.Framework;

namespace NetBDI.Core.UnitTests.BeliefBase
{
    public class WhenUpdateBeliefAndNew
    {
        private Core.BeliefBase _result;
        private readonly Core.Belief _belief = new Core.Belief("name", true);

        [SetUp]
        public void Setup()
        {
            _result = new Core.BeliefBase();
            _result.UpdateBelief(_belief);
        }

        [Test]
        public void ThenBeliefSetIsAddedAndRetrievable()
        {
            Assert.AreEqual(_belief, _result.GetBelief("name"));
        }
    }
}