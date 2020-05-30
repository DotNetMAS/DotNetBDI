using System;
using System.Collections.Generic;

namespace NetBDI.Core
{
    /// <summary>
    /// The current beliefbase holds a database of beliefs and beliefsets
    /// </summary>
    public class BeliefBase
    {
        /// <summary>
        /// The beliefs of this beliefbase as a list of keyvaluepairs with the name of the belief and the actual belief
        /// </summary>
        private readonly Dictionary<string, Belief> _beliefs = new Dictionary<string, Belief>();
        
        /// <summary>
        /// Tthe beliefsets of this beliefbase as a list of keyvaluepairs with the name of the beliefset and the actual beliefset
        /// </summary>
        private readonly Dictionary<string, BeliefSet> _beliefSets = new Dictionary<string, BeliefSet>();

        /// <summary>
        /// The update of a belief with a new belief
        /// </summary>
        /// <param name="belief">The belief to be updated</param>
        public void UpdateBelief(Belief belief)
        {
            if (!_beliefs.ContainsKey(belief.Name))
                _beliefs.Add(belief.Name, belief);
            else
                _beliefs[belief.Name] = belief;
        }

        /// <summary>
        /// Get belief by name
        /// </summary>
        /// <param name="name">The name of the belief to retrieve</param>
        /// <returns>The belief in this beliefbase with the given name</returns>
        public Belief GetBelief(string name)
        {
            if (_beliefs.ContainsKey(name))
                return _beliefs[name];
            else
                return null;
        }

        /// <summary>
        /// Add a new beliefset to the beliefbase
        /// </summary>
        /// <param name="beliefSet">The beliefset to add to the beliefbase</param>
        public void AddBeliefSet(BeliefSet beliefSet)
        {
            if (!_beliefSets.ContainsKey(beliefSet.Name))
                _beliefSets.Add(beliefSet.Name, beliefSet);
        }

        /// <summary>
        /// Updates a beliefset in this beliefbase
        /// </summary>
        /// <param name="beliefSet">The beliefset to update</param>
        public void UpdateBeliefSet(BeliefSet beliefSet)
        {
            if (!_beliefSets.ContainsKey(beliefSet.Name))
                _beliefSets.Add(beliefSet.Name, beliefSet);
            else
                _beliefSets[beliefSet.Name] = beliefSet;
        }

        /// <summary>
        /// Gets a beliefset by name
        /// </summary>
        /// <param name="name">The name of the beliefset we wish to retrieve</param>
        /// <returns>The beliefset with the given name</returns>
        public BeliefSet GetBeliefSet(string name)
        {
            if (_beliefSets.ContainsKey(name))
                return _beliefSets[name];
            else
                return null;
        }
    }
}