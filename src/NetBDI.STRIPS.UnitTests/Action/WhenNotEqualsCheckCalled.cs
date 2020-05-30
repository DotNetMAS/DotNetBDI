using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class WhenNotEqualsCheckCalled
    {
        private Func<Dictionary<string, object>, bool> _result;

        [SetUp]
        public void Setup()
        {
            _result = STRIPS.Action.NotEqualsCheck(new STRIPS.NamedParameter("param1"), new STRIPS.NamedParameter("param2"));
        }

        [Test]
        public void ThenReturnsAFunctionThatReturnsTrueIfOneOfNamedParametersNotFound()
        {
            var blnResult = _result(new Dictionary<string, object> { { "param2", new object() } });
            Assert.True(blnResult);
            blnResult = _result(new Dictionary<string, object> { { "param1", new object() } });
            Assert.True(blnResult);
        }

        [Test]
        public void ThenReturnsAFunctionThatReturnsTrueIfParametersInAssignmentDifferent()
        {
            var blnResult = _result(new Dictionary<string, object> { { "param1", new object() }, { "param2", "test" } });
            Assert.True(blnResult);
        }

        [Test]
        public void ThenReturnsAFunctionThatReturnsFalseIfParametersInAssignmentSame()
        {
            var obj = new object();
            var blnResult = _result(new Dictionary<string, object> { { "param1", obj }, { "param2", obj } });
            Assert.False(blnResult);
        }

        [Test]
        public void ThenReturnsAFunctionThatReturnsTrueIfParametersInAssignmentBothNull()
        {
            var blnResult = _result(new Dictionary<string, object> { { "param1", null}, { "param2", null } });
            Assert.True(blnResult);
        }
    }
}