using NetBDI.STRIPS;

namespace NetBDI.Core
{
    /// <summary>
    /// An intention of our agent
    /// </summary>
    public class Intention<TAction, TAgent, TEnvironment>
        where TAction : STRIPS.Action
        where TAgent : Agent<TAction, TAgent, TEnvironment>
        where TEnvironment : IEnvironment
    {
        /// <summary>
        /// The desire this intention is based upon
        /// </summary>
        public Desire<TAction, TAgent, TEnvironment> Desire { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="desire">The desire upon which this intention is based upon</param>
        public Intention(Desire<TAction, TAgent, TEnvironment> desire)
        {
            Desire = desire;
        }

        /// <summary>
        /// Retrieve the strength of the intention/desire
        /// </summary>
        /// <returns>The strength of the underlying desire</returns>
        public int GetStrength() => Desire.Strength;

        /// <summary>
        /// We create a complex goal for this intention based on the complex goal of the desire 
        /// </summary>
        /// <returns>The created complex goal</returns>
        internal ComplexGoal CreateGoal() => Desire.CreateGoal();

        /// <summary>
        /// Test to see if the intention is fulfilled
        /// </summary>
        /// <returns>True if the underlying desire is fulfilled, false otherwise</returns>
        internal bool IsFulfilled() => Desire.IsFulfilled();

        /// <summary>
        /// Test to see if the intention is achievable
        /// </summary>
        /// <returns>True if the underlying desire is achievable, false otherwise</returns>
        internal bool IsAchievable() => Desire.IsAchievable();

        /// <summary>
        /// Is a desire compatible and achievable in combination with this desire/intention
        /// </summary>
        /// <param name="otherDesire">Other desire to test</param>
        /// <returns>True if compatible, false otherwise</returns>
        internal bool IsCompatibleWith(Desire<TAction, TAgent, TEnvironment> otherDesire) => Desire.IsCompatibleWith(otherDesire);
    }
}