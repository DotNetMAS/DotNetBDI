using NetBDI.STRIPS.UnitTests.Action;
using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.SimpleGoal
{
    public class WhenCreatedWithAction
    {
        private STRIPS.SimpleGoal _result;
        private STRIPS.Fact _fact;
        private STRIPS.Action _action;

        [SetUp]
        public void Setup()
        {
            _fact = new STRIPS.Fact("name");
            _action = new SomeAction();
            _result = new STRIPS.SimpleGoal(_fact, _action);
        }

        [Test]
        public void ThenFactIsSet()
        {
            Assert.AreEqual(_fact, _result.Fact);
        }

        [Test]
        public void ThenActionIsSet()
        {
            Assert.AreEqual(_action, _result.Action);
        }
    }
}