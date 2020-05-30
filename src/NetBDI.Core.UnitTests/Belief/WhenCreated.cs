using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Belief
{
    public class WhenCreated
    {
        private Core.Belief _result;

        [SetUp]
        public void Setup()
        {
            _result = new Core.Belief("name", "test");
        }

        [Test]
        public void ThenNameIsSet()
        {
            Assert.AreEqual("name", _result.Name);
        }

        [Test]
        public void ThenValueIsSet()
        {
            Assert.AreEqual("test", _result.Value);
            Assert.AreEqual("test", _result.GetValue<string>());
        }
    }
}