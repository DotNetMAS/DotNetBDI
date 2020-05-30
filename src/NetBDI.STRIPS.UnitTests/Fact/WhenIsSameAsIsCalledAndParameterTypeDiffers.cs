using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenIsSameAsIsCalledAndParameterTypeDiffers
    {
        private bool _result;
        private bool _result2;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name", new STRIPS.ValueParameter(null));
            _result = fact.IsSameAs(new STRIPS.Fact("name", new STRIPS.NamedParameter("test")));

            var fact2 = new STRIPS.Fact("name", new STRIPS.NamedParameter("test"));
            _result2 = fact2.IsSameAs(new STRIPS.Fact("name", new STRIPS.ValueParameter(null)));
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.False(_result);
            Assert.False(_result2);
        }
    }
}