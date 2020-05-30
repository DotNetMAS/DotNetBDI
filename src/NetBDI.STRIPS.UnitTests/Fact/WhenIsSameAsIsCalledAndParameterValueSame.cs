using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenIsSameAsIsCalledAndParameterValueSame
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var fact = new STRIPS.Fact("name", new STRIPS.ValueParameter("test"));
            _result = fact.IsSameAs(new STRIPS.Fact("name", new STRIPS.ValueParameter("test")));
        }

        [Test]
        public void ThenResultIsTrue()
        {
            Assert.True(_result);
        }
    }
}