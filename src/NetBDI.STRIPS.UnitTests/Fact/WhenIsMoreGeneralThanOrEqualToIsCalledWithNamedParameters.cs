using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenIsMoreGeneralThanOrEqualToIsCalledWithNamedParameters
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name", new STRIPS.NamedParameter("blurp"));
            _result = fact.IsMoreGeneralThanOrEqualTo(new STRIPS.Fact("name", new STRIPS.NamedParameter("test")));
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.True(_result);
        }
    }
}