using System.Collections.Generic;

namespace NetBDI.STRIPS
{
    /// <summary>
    /// This class is mainly used in loop-detection in the planner.
    /// In the planner we keep a list of states to see if by choosing a certain action as a solution for a 
    /// goal wil actually generate a state which we already have visited
    /// This loop detection is performed in the goal decomposition phase of a complex goal
    /// 
    /// To properly detect a loop, we need to keep track of the current beliefs at that time and the complex goal being decomposed.
    /// State are both checked AND created at goal decomposition
    /// </summary>
    public class State
    {
        /// <summary>
        /// Current beliefs of the state
        /// </summary>
        public List<Fact> CurrentBeliefs { get; set; }
        /// <summary>
        /// Current complex goal of the state
        /// </summary>
        public ComplexGoal CurrentComplexGoal { get; set; }
    }
}