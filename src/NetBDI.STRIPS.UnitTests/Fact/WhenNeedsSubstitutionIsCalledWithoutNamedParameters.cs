using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    class WhenNeedsSubstitutionIsCalledWithoutNamedParameter
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name", new STRIPS.ValueParameter("test"));
            _result = fact.NeedsSubstitution();
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.False(_result);
        }
    }
}
