using System.Collections.Generic;
using System.Linq;

namespace NetBDI.STRIPS
{
    /// <summary>
    /// A complex goal containing a list of individual subgoals
    /// </summary>
    public class ComplexGoal : IGoal
    {
        /// <summary>
        /// The list of subgoals
        /// </summary>
        public List<SimpleGoal> Goals { get; } = new List<SimpleGoal>();

        /// <summary>
        /// Check if a complex goal is fulfilled. This is only the case when all subgoals have been fulfilled.
        /// </summary>
        /// <param name="currentBeliefs">The current beliefs</param>
        /// <returns>True if all subgoals are fulfilled, false otherwise</returns>
        public bool IsFulFilled(List<Fact> currentBeliefs) => Goals.All(x => x.IsFulFilled(currentBeliefs));

        /// <summary>
        /// Check to see if a complex goal is the same as another
        /// </summary>
        /// <param name="goal">The other complex goal to test for equality</param>
        /// <returns>True if they have the same amount of subgoals and if every subgoal 
        /// in its ordered list is the same goal in the ordered list of the other complex goal</returns>
        internal bool IsSame(ComplexGoal goal)
        {
            //not same amount of goals so can't be equal
            if (goal.Goals.Count() != Goals.Count())
                return false;

            //test if all subgoals (ordered) match (return false if one set does not match)
            for (var i = 0; i < goal.Goals.Count(); i++)
                if (!goal.Goals[i].Fact.IsSameAs(Goals[i].Fact))
                    return false;

            return true;
        }
    }
}