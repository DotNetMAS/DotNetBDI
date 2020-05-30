using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenCreated
    {
        private STRIPS.Fact _result;
        private readonly STRIPS.ValueParameter _param = new STRIPS.ValueParameter(null);
        private readonly STRIPS.NamedParameter _param2 = new STRIPS.NamedParameter("test");

        [SetUp]
        public void Setup()
        {
            _result = new STRIPS.Fact("name", _param, _param2);
        }

        [Test]
        public void ThenNameIsSet()
        {
            Assert.AreEqual("name", _result.Name);
        }

        [Test]
        public void ThenParametersAreSet()
        {
            Assert.AreEqual(2, _result.Parameters.Count);
            Assert.AreEqual(_param, _result.Parameters[0]);
            Assert.AreEqual(_param2, _result.Parameters[1]);
        }
    }
}