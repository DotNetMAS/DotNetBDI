using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    class WhenNeedsSubstitutionIsCalledWithNamedParameter
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name", new STRIPS.NamedParameter("test"));
            _result = fact.NeedsSubstitution();
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.True(_result);
        }
    }
}
