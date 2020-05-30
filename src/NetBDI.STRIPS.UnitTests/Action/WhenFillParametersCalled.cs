using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class WhenFillParametersCalled
    {
        private class AnAction : STRIPS.Action
        {
            public AnAction()
            {
                AddList.Add(new STRIPS.Fact("test", new STRIPS.NamedParameter("param1")));
                DeleteList.Add(new STRIPS.Fact("test", new STRIPS.NamedParameter("param1")));
                Preconditions.Add(new STRIPS.Fact("test", new STRIPS.NamedParameter("param1")));
            }

            public override STRIPS.Action Clone()
            {
                throw new System.NotImplementedException();
            }
        }

        private AnAction _result;
        private readonly object _obj = new object();

        [SetUp]
        public void Setup()
        {
            _result = new AnAction();
            _result.FillParameters(new Dictionary<string, object> { { "param1", _obj } });
        }

        [Test]
        public void ThenAllVariablesAreFilledWithTheSubstitution()
        {
            Assert.NotNull(_result.AddList[0].Parameters[0] as STRIPS.ValueParameter);
            Assert.AreEqual(_obj, (_result.AddList[0].Parameters[0] as STRIPS.ValueParameter).Value);

            Assert.NotNull(_result.DeleteList[0].Parameters[0] as STRIPS.ValueParameter);
            Assert.AreEqual(_obj, (_result.DeleteList[0].Parameters[0] as STRIPS.ValueParameter).Value);

            Assert.NotNull(_result.Preconditions[0].Parameters[0] as STRIPS.ValueParameter);
            Assert.AreEqual(_obj, (_result.Preconditions[0].Parameters[0] as STRIPS.ValueParameter).Value);
        }
    }
}