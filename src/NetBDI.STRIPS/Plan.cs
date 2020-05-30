using System.Collections.Generic;
using System.Linq;

namespace NetBDI.STRIPS
{
    /// <summary>
    /// The end or intermediate result of our planning.
    /// A plan consists of an ordered list of individual steps which are fully instantiated actions that can be executed.
    /// </summary>
    public class Plan<TAction> where TAction : Action
    {
        /// <summary>
        /// The steps of the plan
        /// </summary>
        public List<TAction> Steps { get; } = new List<TAction>();

        /// <summary>
        /// Check to see if this plan actually has steps left to perform
        /// </summary>
        /// <returns>True if we have steps left, otherwise false</returns>
        public bool HasSteps() => Steps.Count > 0;

        /// <summary>
        /// Retrieve the first step of the plan
        /// </summary>
        /// <returns>The first step of the plan</returns>
        public TAction GetFirstStep() => Steps.First();

        /// <summary>
        /// We finish the step and remove it from the plan as the plan can only contains steps that haven't been executed yet.
        /// </summary>
        /// <param name="step">The finished step to remove</param>
        public void FinishStep(TAction step) => Steps.Remove(step);
    }
}