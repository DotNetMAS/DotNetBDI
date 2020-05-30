using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class WhenGetAssignmentCalled
    {
        private Dictionary<string, object> _result;
        private readonly object _obj = new object();

        [SetUp]
        public void Setup()
        {
            var act = new SomeAction();
            _result = act.GetAssignment(
                new STRIPS.Fact("name", new STRIPS.ValueParameter(null), new STRIPS.NamedParameter("param1"), new STRIPS.NamedParameter("param2")),
                new STRIPS.SimpleGoal(new STRIPS.Fact("name", new STRIPS.ValueParameter("test"), new STRIPS.ValueParameter(_obj))), new List<STRIPS.Fact>());
        }

        [Test]
        public void ThenReturnsAFilledAssignment()
        {
            Assert.IsNotEmpty(_result);
            Assert.AreEqual(1, _result.Count);
            Assert.True(_result.ContainsKey("param1"));
            Assert.AreEqual(_obj, _result["param1"]);
        }
    }
}