using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace NetBDI.STRIPS.UnitTests.Planner
{

    public class WhenCreatePlanCalled
    {
        private STRIPS.Plan<STRIPS.Action> _result;
        private List<STRIPS.Fact> _beliefs;
        private STRIPS.ComplexGoal _complexGoal;

        private STRIPS.Plan<STRIPS.Action> _result2;
        private List<STRIPS.Fact> _beliefs2;
        private STRIPS.ComplexGoal _complexGoal2;

        private STRIPS.Plan<STRIPS.Action> _result3;
        private List<STRIPS.Fact> _beliefs3;
        private STRIPS.ComplexGoal _complexGoal3;

        [SetUp]
        public void Setup()
        {
            var table = new Table();
            var actions = new List<STRIPS.Action>
            {
                new PickupAction(table),
                new PutDownAction(table),
                new StackAction(),
                new UnstackAction()
            };
            var black = new Block(Color.Black, table);
            var red = new Block(Color.Red, table);
            var yellow = new Block(Color.Yellow, red);
            var green = new Block(Color.Green, black);

            var itemsToProcess = new Stack<IStackItem>();
            _complexGoal = new STRIPS.ComplexGoal();
            _complexGoal.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(black), new STRIPS.ValueParameter(green))));
            _complexGoal.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(green), new STRIPS.ValueParameter(table))));
            itemsToProcess.Push(_complexGoal);
            var currentBeliefs = new List<STRIPS.Fact>
            {
                new STRIPS.Fact(Definitions.ArmHolds, new STRIPS.ValueParameter(null)),
                new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(black), new STRIPS.ValueParameter(table)),
                new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(red), new STRIPS.ValueParameter(table)),
                new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(green), new STRIPS.ValueParameter(black)),
                new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(yellow), new STRIPS.ValueParameter(red)),
                new STRIPS.Fact(Definitions.Clear, new STRIPS.ValueParameter(green)),
                new STRIPS.Fact(Definitions.Clear, new STRIPS.ValueParameter(yellow))
            };
            (_result, _beliefs) = STRIPS.Planner.CreatePlan<STRIPS.Action>(itemsToProcess, currentBeliefs.ToList(), new List<STRIPS.Fact>(), new List<State>(), actions);

            var itemsToProcess2 = new Stack<IStackItem>();
            _complexGoal2 = new STRIPS.ComplexGoal();
            _complexGoal2.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(black), new STRIPS.ValueParameter(green))));
            _complexGoal2.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(green), new STRIPS.ValueParameter(table))));
            itemsToProcess2.Push(_complexGoal2);
            (_result2, _beliefs2) = STRIPS.Planner.CreatePlan<STRIPS.Action>(itemsToProcess2, currentBeliefs.ToList(), new List<STRIPS.Fact>(), new List<State>(), actions);

            var itemsToProcess3 = new Stack<IStackItem>();
            _complexGoal3 = new STRIPS.ComplexGoal();
            _complexGoal3.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact(Definitions.Clear, new STRIPS.ValueParameter(yellow))));
            _complexGoal3.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(yellow), new STRIPS.ValueParameter(red))));
            _complexGoal3.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(red), new STRIPS.ValueParameter(black))));
            _complexGoal3.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(black), new STRIPS.ValueParameter(green))));
            _complexGoal3.Goals.Add(new STRIPS.SimpleGoal(new STRIPS.Fact(Definitions.On, new STRIPS.ValueParameter(green), new STRIPS.ValueParameter(table))));
            itemsToProcess3.Push(_complexGoal3);
            (_result3, _beliefs3) = STRIPS.Planner.CreatePlan<STRIPS.Action>(itemsToProcess3, currentBeliefs.ToList(), new List<STRIPS.Fact>(), new List<State>(), actions);
        }

        [Test]
        public void ThenAProperPlanIsCreated()
        {
            Assert.AreEqual("Unstack(Green, Black)", _result.Steps[0].ToString());
            Assert.AreEqual("PutDown(Green, table)", _result.Steps[1].ToString());
            Assert.AreEqual("PickUp(Black, table)", _result.Steps[2].ToString());
            Assert.AreEqual("Stack(Black, Green)", _result.Steps[3].ToString());

            //Assert.True(_beliefs.Any(x => x.IsSameAs(_complexGoal.Goals[0].Fact)));
            //Assert.True(_beliefs.Any(x => x.IsSameAs(_complexGoal.Goals[1].Fact)));

            Assert.AreEqual("Unstack(Green, Black)", _result2.Steps[0].ToString());
            Assert.AreEqual("PutDown(Green, table)", _result2.Steps[1].ToString());
            Assert.AreEqual("PickUp(Black, table)", _result2.Steps[2].ToString());
            Assert.AreEqual("Stack(Black, Green)", _result2.Steps[3].ToString());

            //Assert.True(_beliefs2.Any(x => x.IsSameAs(_complexGoal2.Goals[0].Fact)));
            //Assert.True(_beliefs2.Any(x => x.IsSameAs(_complexGoal2.Goals[1].Fact)));

            Assert.AreEqual("Unstack(Green, Black)", _result3.Steps[0].ToString());
            Assert.AreEqual("PutDown(Green, table)", _result3.Steps[1].ToString());
            Assert.AreEqual("PickUp(Black, table)", _result3.Steps[2].ToString());
            Assert.AreEqual("Stack(Black, Green)", _result3.Steps[3].ToString());
            Assert.AreEqual("Unstack(Yellow, Red)", _result3.Steps[4].ToString());
            Assert.AreEqual("PutDown(Yellow, table)", _result3.Steps[5].ToString());
            Assert.AreEqual("PickUp(Red, table)", _result3.Steps[6].ToString());
            Assert.AreEqual("Stack(Red, Black)", _result3.Steps[7].ToString());
            Assert.AreEqual("PickUp(Yellow, table)", _result3.Steps[8].ToString());
            Assert.AreEqual("Stack(Yellow, Red)", _result3.Steps[9].ToString());

            //Assert.True(_beliefs3.Any(x => x.IsSameAs(_complexGoal3.Goals[0].Fact)));
            //Assert.True(_beliefs3.Any(x => x.IsSameAs(_complexGoal3.Goals[1].Fact)));
            //Assert.True(_beliefs3.Any(x => x.IsSameAs(_complexGoal3.Goals[2].Fact)));
            //Assert.True(_beliefs3.Any(x => x.IsSameAs(_complexGoal3.Goals[3].Fact)));
            //Assert.True(_beliefs3.Any(x => x.IsSameAs(_complexGoal3.Goals[4].Fact)));
        }

        public interface IElement
        {
            public Color Color { get; set; }
            public void AddBlock(Block block);
            public void RemoveBlock(Block block);
        }

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        public class Table : IElement
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        {
            public List<Block> Blocks { get; set; } = new List<Block>();
            public Color Color { get; set; }

            public void AddBlock(Block block)
            {
                Blocks.Add(block);
            }

            public void RemoveBlock(Block block)
            {
                Blocks.Remove(block);
            }

            public override bool Equals(object obj) => obj is Table; //there is only one table
        }

        public class Block : IElement
        {
            public IElement Lower { get; set; }

            public Color Color { get; set; }

            public Block Upper { get; set; }

            public Block(Color color, IElement lower)
            {
                Color = color;
                lower.AddBlock(this);
                Lower = lower;
            }

            public void AddBlock(Block block)
            {
                Upper = block;
            }

            public void RemoveBlock(Block block)
            {
                if (Upper == block)
                    Upper = null;
            }

            public override string ToString() => Color.Name;
        }

        private static class Definitions
        {
            public static readonly string ArmHolds = "ArmHolds";
            public static readonly string On = "On";
            public static readonly string Clear = "Clear";
        }

        private class UnstackAction : STRIPS.Action
        {
            public UnstackAction()
            {
                var param1 = new STRIPS.NamedParameter("param1");
                var param2 = new STRIPS.NamedParameter("param2");
                var goal1 = new STRIPS.Fact(Definitions.On, param1, param2);
                var goal2 = new STRIPS.Fact(Definitions.ArmHolds, new STRIPS.ValueParameter(null));
                var goal3 = new STRIPS.Fact(Definitions.Clear, param1);
                Preconditions.Add(goal2);
                Preconditions.Add(goal1);
                Preconditions.Add(goal3);
                AddList.Add(new STRIPS.Fact(Definitions.ArmHolds, param1));
                AddList.Add(new STRIPS.Fact(Definitions.Clear, param2));
                DeleteList.Add(goal1);
                DeleteList.Add(goal2);
                DeleteList.Add(goal3);
                Constraints.Add(TypeCheck(typeof(Block), param1));
                Constraints.Add(TypeCheck(typeof(Block), param2));
                Constraints.Add(NotEqualsCheck(param1, param2));
            }
            public override STRIPS.Action Clone() => new UnstackAction();
            public override string ToString() => "Unstack(" + Preconditions[1].Parameters[0].ToString() + ", " + Preconditions[1].Parameters[1].ToString() + ")";
        }

        private class StackAction : STRIPS.Action
        {
            public StackAction()
            {
                var param1 = new STRIPS.NamedParameter("param1");
                var param2 = new STRIPS.NamedParameter("param2");
                var goal1 = new STRIPS.Fact(Definitions.ArmHolds, param1);
                var goal2 = new STRIPS.Fact(Definitions.Clear, param2);
                Preconditions.Add(goal1);
                Preconditions.Add(goal2);
                AddList.Add(new STRIPS.Fact(Definitions.ArmHolds, new STRIPS.ValueParameter(null)));
                AddList.Add(new STRIPS.Fact(Definitions.On, param1, param2));
                AddList.Add(new STRIPS.Fact(Definitions.Clear, param1));
                DeleteList.Add(goal1);
                DeleteList.Add(goal2);
                Constraints.Add(TypeCheck(typeof(Block), param1));
                Constraints.Add(TypeCheck(typeof(Block), param2));
                Constraints.Add(NotEqualsCheck(param1, param2));
            }

            public override STRIPS.Action Clone() => new StackAction();
            public override string ToString() => "Stack(" + Preconditions[0].Parameters[0].ToString() + ", " + Preconditions[1].Parameters[0].ToString() + ")";
        }

        public class PutDownAction : STRIPS.Action
        {
            private readonly Table _table;

            public PutDownAction(Table table)
            {
                _table = table;
                var block = new STRIPS.NamedParameter("param1");
                var goal1 = new STRIPS.Fact(Definitions.ArmHolds, block);
                Preconditions.Add(goal1);
                AddList.Add(new STRIPS.Fact(Definitions.ArmHolds, new STRIPS.ValueParameter(null)));
                AddList.Add(new STRIPS.Fact(Definitions.On, block, new STRIPS.ValueParameter(table)));
                AddList.Add(new STRIPS.Fact(Definitions.Clear, block));
                DeleteList.Add(goal1);
                Constraints.Add(TypeCheck(typeof(Block), block));
            }

            public override STRIPS.Action Clone() => new PutDownAction(_table);
            public override string ToString() => "PutDown(" + DeleteList[0].Parameters[0].ToString() + ", table)";
        }

        private class PickupAction : STRIPS.Action
        {
            private readonly Table _table;

            /// <summary>
            /// Default constructor that initializes preconditions, add/delete list and constraints
            /// </summary>
            public PickupAction(Table table)
            {
                _table = table;
                var block = new STRIPS.NamedParameter("param1");
                var goal1 = new STRIPS.Fact(Definitions.On, block, new STRIPS.ValueParameter(table));
                var goal2 = new STRIPS.Fact(Definitions.ArmHolds, new STRIPS.ValueParameter(null));
                var goal3 = new STRIPS.Fact(Definitions.Clear, block);
                Preconditions.Add(goal2);
                Preconditions.Add(goal1);
                Preconditions.Add(new STRIPS.Fact(Definitions.Clear, block));
                AddList.Add(new STRIPS.Fact(Definitions.ArmHolds, block));
                DeleteList.Add(goal1);
                DeleteList.Add(goal2);
                DeleteList.Add(goal3);
                Constraints.Add(TypeCheck(typeof(Block), block));
            }

            public override STRIPS.Action Clone() => new PickupAction(_table);
            public override string ToString() => "PickUp(" + DeleteList[2].Parameters[0].ToString() + ", table)";
        }
    }
}