using System.Threading.Tasks;

namespace NetBDI.STRIPS.UnitTests.Action
{
    public class SomeOtherAction : STRIPS.Action
    { 
        public SomeOtherAction()
        {
            AddList.Add(new STRIPS.Fact("True"));
            AddList.Add(new STRIPS.Fact("OneParam", new STRIPS.NamedParameter("test")));
            Constraints.Add(TypeCheck(typeof(string), new STRIPS.NamedParameter("test")));
        }

        public override STRIPS.Action Clone() => new SomeAction();
    }
}