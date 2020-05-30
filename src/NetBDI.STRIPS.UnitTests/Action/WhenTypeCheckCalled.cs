using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class WhenTypeCheckCalled
    {
        private Func<Dictionary<string, object>, bool> _result;

        [SetUp]
        public void Setup()
        {
            _result = STRIPS.Action.TypeCheck(new object().GetType(), new STRIPS.NamedParameter("param1"));
        }

        [Test]
        public void ThenReturnsAFunctionThatReturnsTrueIfNamedParameterNotFound()
        {
            var blnResult = _result(new Dictionary<string, object> { { "param2", new object() } });
            Assert.True(blnResult);
        }

        [Test]
        public void ThenReturnsAFunctionThatReturnsFalseIfNamedParameterInAssignmentIsNull()
        {
            var blnResult = _result(new Dictionary<string, object> { { "param1", null } });
            Assert.False(blnResult);
        }

        [Test]
        public void ThenReturnsAFunctionThatReturnsFalseIfNamedParameterInAssignmentIsOfWrongType()
        {
            var blnResult = _result(new Dictionary<string, object> { { "param1", "blurp" } });
            Assert.False(blnResult);
        }

        [Test]
        public void ThenReturnsAFunctionThatReturnsTrueIfNamedParameterInAssignmentIsOfRightType()
        {
            var blnResult = _result(new Dictionary<string, object> { { "param1", new object() } });
            Assert.True(blnResult);
        }
    }
}