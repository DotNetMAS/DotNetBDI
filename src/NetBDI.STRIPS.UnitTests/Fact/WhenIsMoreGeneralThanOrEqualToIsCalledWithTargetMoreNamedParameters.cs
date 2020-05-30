using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenIsMoreGeneralThanOrEqualToIsCalledWithTargetMoreNamedParameters
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name", new STRIPS.ValueParameter("blurp"));
            _result = fact.IsMoreGeneralThanOrEqualTo(new STRIPS.Fact("name", new STRIPS.NamedParameter("test")));
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.False(_result);
        }
    }
}