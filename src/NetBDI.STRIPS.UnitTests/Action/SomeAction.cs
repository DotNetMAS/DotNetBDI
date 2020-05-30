using System.Threading.Tasks;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class SomeAction : STRIPS.Action
    { 
        public SomeAction()
        {
            AddList.Add(new STRIPS.Fact("True"));
            AddList.Add(new STRIPS.Fact("OneParam", new STRIPS.NamedParameter("test")));
        }

        public override STRIPS.Action Clone() => new SomeAction();
    }
}