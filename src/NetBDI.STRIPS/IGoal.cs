using System.Collections.Generic;

namespace NetBDI.STRIPS
{
    /// <summary>
    /// An interface for a goal which can be processed in the planner
    /// </summary>
    public interface IGoal : IStackItem
    {
        /// <summary>
        /// Checks if the goal is fulfilled in the current set of beliefs
        /// </summary>
        /// <param name="currentBeliefs">The current beliefs that are used to check the possible end states of a goal</param>
        /// <returns>True if the goal is fulfulled, false otherwise</returns>
        bool IsFulFilled(List<Fact> currentBeliefs);
    }
}