using System.Collections.Generic;

namespace NetBDI.Core
{
    /// <summary>
    /// A beliefset containing a list of values
    /// </summary>
    public class BeliefSet
    {
        /// <summary>
        /// The name of the beliefset
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The list of values of the beliefset
        /// </summary>
        public List<object> Values { get; } = new List<object>();

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">The name of the beliefset</param>
        public BeliefSet(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Adds a fact to the beliefset
        /// </summary>
        /// <param name="obj">The fact (or simply an object) to add to the beliefset</param>
        public void AddFact(object obj)
        {
            Values.Add(obj);
        }
    }
}