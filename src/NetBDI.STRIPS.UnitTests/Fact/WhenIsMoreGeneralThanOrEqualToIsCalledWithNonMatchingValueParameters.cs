using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenIsMoreGeneralThanOrEqualToIsCalledWithNonMatchingValueParameters
    {
        private bool _result;
        private bool _result2;
        private bool _result3;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name", new STRIPS.ValueParameter(null));
            _result = fact.IsMoreGeneralThanOrEqualTo(new STRIPS.Fact("name", new STRIPS.ValueParameter("test")));

            var fact2 = new STRIPS.Fact("name", new STRIPS.ValueParameter("test"));
            _result2 = fact2.IsMoreGeneralThanOrEqualTo(new STRIPS.Fact("name", new STRIPS.ValueParameter(null)));

            var fact3 = new STRIPS.Fact("name", new STRIPS.ValueParameter("test"));
            _result3 = fact3.IsMoreGeneralThanOrEqualTo(new STRIPS.Fact("name", new STRIPS.ValueParameter("test2")));
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.False(_result);
            Assert.False(_result2);
            Assert.False(_result3);
        }
    }
}