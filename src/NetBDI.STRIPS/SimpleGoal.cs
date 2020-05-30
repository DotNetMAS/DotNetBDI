using System.Collections.Generic;
using System.Linq;

namespace NetBDI.STRIPS
{
    /// <summary>
    /// A class that holds a singular goal as a fact.
    /// A goal can be linked to an action so that when doing substitution on the parameters of a goal, 
    /// we can also perform substitution on the linked action as a whole so that the add and delete list and the preconditions
    /// are also subjected to that substitution
    /// </summary>
    public class SimpleGoal : IGoal
    {
        /// <summary>
        /// The fact that needs to be true when the goal is satisfied
        /// </summary>
        public Fact Fact { get; }

        /// <summary>
        /// (Optional) An action that is linked to a goal to perform substitution on the goal AND the linked action
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="fact">The fact that is the goal state</param>
        public SimpleGoal(Fact fact)
        {
            Fact = fact;
        }

        /// <summary>
        /// The constructor with a linked action
        /// </summary>
        /// <param name="fact">The fact that is the goal state</param>
        /// <param name="action">The linked action used in substitution</param>
        public SimpleGoal(Fact fact, STRIPS.Action action)
        {
            Fact = fact;
            Action = action;
        }

        /// <summary>
        /// Checks to see if a simple goal is statisfied by checking the end state in the list of current beliefs
        /// </summary>
        /// <param name="currentBeliefs">The current beliefs being used for checking the end goal</param>
        /// <returns>True if the end state (fact) is present in the list of current beliefs AND is fully instantiated, false otherwise</returns>
        public virtual bool IsFulFilled(List<Fact> currentBeliefs)
        {
            //end goal needs to be fully instantiated before it can be fulfilled
            if (!Fact.Parameters.All(x => x as ValueParameter != null))
                return false;

            return currentBeliefs.Any(x => x.Name.Equals(Fact.Name) && x.IsSameAs(Fact));
        }

        /// <summary>
        /// Check to see if this goal has a named parameter (parameter which is not instantiated)
        /// </summary>
        /// <returns>True if the end goal/fact is fully instantiated, false otherwise</returns>
        internal bool NeedsSubstitution() => Fact.NeedsSubstitution();
    }
}