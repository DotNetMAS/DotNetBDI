using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.NamedParameter
{
    public class WhenCreated
    {
        private STRIPS.NamedParameter _result;

        [SetUp]
        public void Setup()
        {
            _result = new STRIPS.NamedParameter("name");
        }

        [Test]
        public void ThenNameIsSet()
        {
            Assert.AreEqual("name", _result.Name);
        }

        [Test]
        public void ToStringReturnsTheName()
        {
            Assert.AreEqual(_result.Name, _result.ToString());
        }
    }
}