using Moq;
using NetBDI.STRIPS;
using NUnit.Framework;

namespace NetBDI.Core.UnitTests.Intention
{
    public class WhenGetStrength
    {
        private int _result;
        private readonly ADesire _desire = new ADesire();

        [SetUp]
        public void Setup()
        {
            _desire.Strength = 5;
            var intent = new Intention<AnAction, AnAgent, AnEnvironment>(_desire);
            _result = intent.GetStrength();
        }

        [Test]
        public void ThenResultIsTheStrengthOfTheDesire()
        {
            Assert.AreEqual(5, _result);
        }
    }
}