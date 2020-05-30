using NUnit.Framework;

namespace NetBDI.Core.UnitTests.BeliefSet
{
    public class WhenCreated
    {
        private Core.BeliefSet _result;

        [SetUp]
        public void Setup()
        {
            _result = new Core.BeliefSet("name");
        }

        [Test]
        public void ThenNameIsSet()
        {
            Assert.AreEqual("name", _result.Name);
        }

        [Test]
        public void ThenValuesIsEmptyButNotNull()
        {
            Assert.NotNull(_result.Values);
            Assert.IsEmpty(_result.Values);
        }
    }
}