using NetBDI.STRIPS;

namespace NetBDI.Core.UnitTests
{
    public class ADesire : Desire<AnAction, AnAgent, AnEnvironment>
    {
        public ADesire() : base(0) { }
        public ADesire(int strength = 0) : base(strength) { }

        public override ComplexGoal CreateGoal() => new ComplexGoal();
    }
}