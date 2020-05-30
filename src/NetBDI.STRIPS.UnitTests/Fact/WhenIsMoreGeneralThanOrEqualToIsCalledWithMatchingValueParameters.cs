using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenIsMoreGeneralThanOrEqualToIsCalledWithMatchingValueParameters
    {
        private bool _result;
        private bool _result2;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name", new STRIPS.ValueParameter(null));
            _result = fact.IsMoreGeneralThanOrEqualTo(new STRIPS.Fact("name", new STRIPS.ValueParameter(null)));

            var fact2 = new STRIPS.Fact("name", new STRIPS.ValueParameter("test"));
            _result2 = fact2.IsMoreGeneralThanOrEqualTo(new STRIPS.Fact("name", new STRIPS.ValueParameter("test")));
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.True(_result);
            Assert.True(_result2);
        }
    }
}