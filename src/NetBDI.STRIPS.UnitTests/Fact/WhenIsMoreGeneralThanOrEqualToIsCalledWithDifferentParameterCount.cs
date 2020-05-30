using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenIsMoreGeneralThanOrEqualToIsCalledWithDifferentParameterCount
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name1", new STRIPS.ValueParameter(null));
            _result = fact.IsMoreGeneralThanOrEqualTo(new STRIPS.Fact("name"));
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.False(_result);
        }
    }
}