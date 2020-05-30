namespace NetBDI.Core.UnitTests
{
    public class AnAction : STRIPS.Action
    {
        public override STRIPS.Action Clone() => new AnAction();
    }
}