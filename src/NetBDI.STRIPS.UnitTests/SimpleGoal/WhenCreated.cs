using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.SimpleGoal
{
    public class WhenCreated
    {
        private STRIPS.SimpleGoal _result;
        private STRIPS.Fact _fact;

        [SetUp]
        public void Setup()
        {
            _fact = new STRIPS.Fact("name");
            _result = new STRIPS.SimpleGoal(_fact);
        }

        [Test]
        public void ThenFactIsSet()
        {
            Assert.AreEqual(_fact, _result.Fact);
        }

        [Test]
        public void ThenActionIsNull()
        {
            Assert.IsNull(_result.Action);
        }
    }
}