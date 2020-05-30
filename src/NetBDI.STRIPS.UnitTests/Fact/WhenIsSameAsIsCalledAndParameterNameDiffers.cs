using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenIsSameAsIsCalledAndParameterNameDiffers
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name", new STRIPS.NamedParameter("test"));
            _result = fact.IsSameAs(new STRIPS.Fact("name", new STRIPS.NamedParameter("test2")));
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.False(_result);
        }
    }
}