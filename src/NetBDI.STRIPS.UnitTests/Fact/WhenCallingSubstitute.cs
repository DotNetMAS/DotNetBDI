using NUnit.Framework;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.Fact
{
    public class WhenCallingSubstitute
    {
        private STRIPS.Fact _result;
        private readonly STRIPS.ValueParameter _param = new STRIPS.ValueParameter(null);
        private readonly STRIPS.NamedParameter _param2 = new STRIPS.NamedParameter("test");
        private readonly STRIPS.NamedParameter _param3 = new STRIPS.NamedParameter("test2");
        private readonly object _obj = new object();

        [SetUp]
        public void Setup()
        {
            _result = new STRIPS.Fact("name", _param, _param2, _param3);
            _result.Substitute(new Dictionary<string, object> { { "test", _obj } });
        }

        [Test]
        public void ThenOnlyNamedParameterIsReplacedByValueWhereNameMatches()
        {
            Assert.AreEqual(_param, _result.Parameters[0]);
            Assert.AreEqual(_param.Value, (_result.Parameters[0] as STRIPS.ValueParameter).Value);
            Assert.True(_result.Parameters[1] is STRIPS.ValueParameter);
            Assert.AreEqual(_obj, (_result.Parameters[1] as STRIPS.ValueParameter).Value);
            Assert.AreEqual(_param3, _result.Parameters[2]);
        }
    }
}