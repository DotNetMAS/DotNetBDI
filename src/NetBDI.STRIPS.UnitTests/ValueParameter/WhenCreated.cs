using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.ValueParameter
{
    public class WhenCreated
    {
        private STRIPS.ValueParameter _result;
        private readonly object _obj = "Test";

        [SetUp]
        public void Setup()
        {
            _result = new STRIPS.ValueParameter(_obj);
        }

        [Test]
        public void ThenValueIsSet()
        {
            Assert.AreEqual(_obj, _result.Value);
        }

        [Test]
        public void ToStringReturnsTheValueOfTheObject()
        {
            Assert.AreEqual(_result.Value.ToString(), _result.ToString());
        }
    }
}