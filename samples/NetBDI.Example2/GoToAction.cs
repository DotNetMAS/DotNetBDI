using NetBDI.STRIPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetBDI.Example2
{
    /// <summary>
    /// An action to go to a position
    /// </summary>
    public class GoToAction : GoldMineAction
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GoToAction()
        {
            var x = new NamedParameter("param1");
            var y = new NamedParameter("param2");
            var x2 = new NamedParameter("param3");
            var y2 = new NamedParameter("param4");
            Preconditions.Add(new Fact(Definitions.In, x, y));
            AddList.Add(new Fact(Definitions.In, x2, y2));
            DeleteList.Add(new Fact(Definitions.In, x, y));
        }

        /// <summary>
        /// We create a clone of the action
        /// </summary>
        /// <returns>The created clone</returns>
        public override STRIPS.Action Clone() => new GoToAction();

        /// <summary>
        /// Instantiate an action so an instantiated goal is fulfilled
        /// </summary>
        /// <param name="action">The action to be instantiated</param>
        /// <param name="goal">The goal to be fulfilled</param>
        /// <param name="currentBeliefs">The set of beliefs we currently have</param>
        /// <returns>The instantiated action (based on the given action) that completely fulfills the given goal</returns>
        public override Dictionary<string, object> GetAssignment(Fact fact, SimpleGoal goal, List<Fact> currentBeliefs)
        {
            var dict = base.GetAssignment(fact, goal, currentBeliefs);
            //If the target is chosen, we need to backtrack the starting position towards the current position of the agent
            if(dict.ContainsKey("param3") && dict.ContainsKey("param4"))
            {
                var inBelief = currentBeliefs.Single(x => x.Name.Equals(Definitions.In));
                var beginX = (double)(inBelief.Parameters[0] as ValueParameter).Value;
                var beginY = (double)(inBelief.Parameters[1] as ValueParameter).Value;
                var endX = (double) dict["param3"];
                var endY = (double) dict["param4"];
                var diffX = Math.Abs(endX - beginX);
                var diffY = Math.Abs(endY - beginY);
                var ratio = Math.Sqrt(Math.Pow(diffX + diffY, 2) / (Math.Pow(diffX, 2) + Math.Pow(diffY, 2)));
                ratio /= (diffX + diffY);
                ratio = Math.Min(ratio, 1);
                var travelX = (beginX - endX) * ratio;
                var travelY = (beginY - endY) * ratio;
                dict.Add("param1", endX + travelX);
                dict.Add("param2", endY + travelY);
            }
            return dict;
        }

        /// <summary>
        /// We execute the action by the agent
        /// </summary>
        /// <param name="agent">The agent who executes the action</param>
        /// <returns>A, asynchronous task in which the action is executed</returns>
        public override async Task Execute(GoldMineAgent agent)
        {
            await Task.Delay(2000);
            agent.CurrentX = (double) Assignments["param3"];
            agent.CurrentY = (double) Assignments["param4"];
            Console.WriteLine(ToString());
        }

        /// <summary>
        /// Override of the toString method for proper reporting
        /// </summary>
        /// <returns>A string representation of this action</returns>
        public override string ToString() => "GoTo(" + Assignments["param3"] + ", "
            + Assignments["param4"] + ")";
    }
}