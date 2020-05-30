using NUnit.Framework;
using System.Threading.Tasks;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class WhenCreated
    {
        private class AnAction : STRIPS.Action
        {
            public override STRIPS.Action Clone()
            {
                throw new System.NotImplementedException();
            }
        }

        private AnAction _result;

        [SetUp]
        public void Setup()
        {
            _result = new AnAction();
        }

        [Test]
        public void ThenAllVariablesAreEmptyButNotNull()
        {
            Assert.NotNull(_result.Constraints);
            Assert.NotNull(_result.AddList);
            Assert.NotNull(_result.DeleteList);
            Assert.NotNull(_result.Preconditions);
            
            Assert.IsEmpty(_result.Constraints);
            Assert.IsEmpty(_result.AddList);
            Assert.IsEmpty(_result.DeleteList);
            Assert.IsEmpty(_result.Preconditions);
        }
    }
}