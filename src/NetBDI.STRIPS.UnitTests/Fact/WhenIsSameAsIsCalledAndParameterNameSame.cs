using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenIsSameAsIsCalledAndParameterNameSame
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name", new STRIPS.NamedParameter("test"));
            _result = fact.IsSameAs(new STRIPS.Fact("name", new STRIPS.NamedParameter("test")));
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.True(_result);
        }
    }
}