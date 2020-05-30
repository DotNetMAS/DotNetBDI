using NUnit.Framework;
using System.Linq;

namespace NetBDI.Core.UnitTests.BeliefSet
{
    public class WhenAddFact
    {
        private Core.BeliefSet _result;
        private readonly object _obj = new object();

        [SetUp]
        public void Setup()
        {
            _result = new Core.BeliefSet("name");
            _result.AddFact(_obj);
        }

        [Test]
        public void ThenFactIsAddedToValues()
        {
            Assert.NotNull(_result.Values);
            Assert.IsNotEmpty(_result.Values);
            Assert.AreEqual(1, _result.Values.Count());
            Assert.AreEqual(_obj, _result.Values[0]);
        }
    }
}