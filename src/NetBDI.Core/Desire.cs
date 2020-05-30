using NetBDI.STRIPS;
using System;
using System.Runtime.CompilerServices;

namespace NetBDI.Core
{
    /// <summary>
    /// A desire expressed by the agent
    /// </summary>
    public abstract class Desire<TAction, TAgent, TEnvironment>
        where TAction : STRIPS.Action
        where TAgent : Agent<TAction, TAgent, TEnvironment>
        where TEnvironment : IEnvironment
    {
        /// <summary>
        /// The strength of the desire
        /// </summary>
        public int Strength { get; set; }

        /// <summary>
        /// The agent expressing the desire
        /// </summary>
        public Agent<TAction, TAgent, TEnvironment> Agent { protected get; set; }

        /// <summary>
        /// The precondition for the desire
        /// </summary>
        public Func<bool> Precondition { get; set; }

        /// <summary>
        /// The targetcondition for the desire
        /// </summary>
        public Func<bool> TargetCondition { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="strength">The strength of the desire</param>
        public Desire(int strength = 0)
        {
            Strength = strength;
        }

        /// <summary>
        /// Create a complex goal for this desire
        /// </summary>
        /// <returns>The created complex goal</returns>
        public abstract ComplexGoal CreateGoal();

        /// <summary>
        /// Is a desire achievable
        /// </summary>
        /// <returns>true if the precondition holds or if there is no precondition, false otherwise</returns>
        public virtual bool IsAchievable()
        {
            if (Precondition != null)
                return Precondition();

            return true;
        }

        /// <summary>
        /// Checks if the desire is fulfilled
        /// </summary>
        /// <returns>True if the targetcondition holds or targetcondition is null, false otherwise</returns>
        public virtual bool IsFulfilled()
        {
            if (TargetCondition != null)
                return TargetCondition();

            return false;
        }

        /// <summary>
        /// Is a desire compatible and achievable in combination with this desire/intention
        /// </summary>
        /// <param name="otherDesire">Other desire to test</param>
        /// <returns>False by default</returns>
        public virtual bool IsCompatibleWith(Desire<TAction, TAgent, TEnvironment> otherDesire) => false;
    }
}