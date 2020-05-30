using NUnit.Framework;

namespace NetBDI.STRIPS.UnitTests.ComplexGoal
{
    public class WhenCallingIsSameAndGoalCountDiffers
    {
        private bool _result;

        [SetUp]
        public void Setup()
        {
            var complex1 = new STRIPS.ComplexGoal();
            complex1.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact("tst")));
            complex1.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact("on")));
            var complex2 = new STRIPS.ComplexGoal();
            complex2.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact("tst")));
            _result = complex1.IsSame(complex2);
        }

        [Test]
        public void ThenResultIsFalse()
        {
            Assert.False(_result);
        }
    }
}