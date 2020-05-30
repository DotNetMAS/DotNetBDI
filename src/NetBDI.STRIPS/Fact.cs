using System.Collections.Generic;
using System.Linq;

namespace NetBDI.STRIPS
{
    /// <summary>
    /// A fact containing a possible piece of information on an (real or virtual) environment
    /// A predicate like On(X, Y) can be represented as a fact with the name "On" and two ordered parameters X and Y
    /// </summary>
    public class Fact
    {
        /// <summary>
        /// The name of the fact
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The list of ordered parameters for this fact
        /// </summary>
        public List<IParameter> Parameters { get; private set; } = new List<IParameter>();

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="name">The name of the fact</param>
        /// <param name="parameters">The list of parameters for this fact.</param>
        public Fact(string name, params IParameter[] parameters)
        {
            Name = name;
            Parameters.AddRange(parameters);
        }

        /// <summary>
        /// Check to see if two facts are exactly the same.
        /// This does not take generality in order but requires all parameters to match (either named parameters or value parameters
        /// </summary>
        /// <param name="fact">The fact to test for equality</param>
        /// <returns>True if the facts have the same name and the same value/named parameters, false otherwise</returns>
        public virtual bool IsSameAs(Fact fact)
        {
            //If the name is different, the facts are different
            if (Name != fact.Name)
                return false;

            //if parameter count does not match, return false
            if (Parameters.Count() != fact.Parameters.Count())
                return false;

            for (var i = 0; i < Parameters.Count(); i++)
            {
                //if the type of parameter does not match then they are not the same
                //for a named parameter, the names must match
                //for value parameters, the values must be equal (or both null)
                var val2 = fact.Parameters[i] as ValueParameter;
                var nam1 = Parameters[i] as NamedParameter;
                var nam2 = fact.Parameters[i] as NamedParameter;
                if (!(Parameters[i] is ValueParameter val1))
                {
                    if (val2 == null && !nam1.Name.Equals(nam2.Name))
                        return false;
                    else if (val2 != null)
                        return false;
                }
                else
                {
                    if (val2 == null)
                        return false;
                    else if (!((val1.Value == null && val2.Value == null) || (val1.Value != null && (val1.Value == val2.Value || val1.Value.Equals(val2.Value)))))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// A substitution of one ore more parameters of a fact
        /// </summary>
        /// <param name="substitutionList">A list of keyvalue pairs containing the name of the parameter and the value to be substituted in
        /// all named parameters in this fact that bear the given name</param>
        internal void Substitute(Dictionary<string, object> substitutionList)
        {
            var lstFilled = new List<IParameter>();
            foreach (var parameter in Parameters)
            {
                //if parameter is a valueparameter, it can not be further substituted
                if (parameter as ValueParameter != null)
                    lstFilled.Add(parameter);
                else if (parameter is NamedParameter namedParameter)
                {
                    //replace named parameters that match the name to value parameters with the given object as value
                    if (substitutionList.ContainsKey(namedParameter.Name))
                        lstFilled.Add(new ValueParameter(substitutionList[namedParameter.Name]));
                    else
                        lstFilled.Add(parameter);
                }
            }
            Parameters = lstFilled;
        }

        /// <summary>
        /// Check to see if this fact needs substitution or in other words still contains a named parameter instead of all value parameters
        /// </summary>
        /// <returns>True if this fact contains a named parameter, false otherwise</returns>
        internal virtual bool NeedsSubstitution() => Parameters.Any(x => x as NamedParameter != null);

        /// <summary>
        /// Check to see if this fact is more general or equally general than a given fact.
        /// A fact is equally general if both contain the same value parameters and the same named parameters
        /// A fact is more general if that fact contains value parameters that match the other fact AND has one or more unfilled named parameters
        /// </summary>
        /// <param name="fact">The fact to be used in the check for generality</param>
        /// <returns>True if this fact is more general (or equally so), false otherwise</returns>
        internal bool IsMoreGeneralThanOrEqualTo(Fact fact)
        {
            if (!fact.Name.Equals(Name))
                return false;

            if (!fact.Parameters.Count.Equals(Parameters.Count))
                return false;

            for (var i = 0; i < Parameters.Count(); i++)
            {
                if (fact.Parameters[i] is ValueParameter valueParameter)
                {
                    //if they have different value parameters, return false
                    if (Parameters[i] is ValueParameter valueParameter2)
                        if (!((valueParameter.Value == null && valueParameter2.Value == null) 
                            || (valueParameter.Value != null && (valueParameter.Value == valueParameter2.Value || valueParameter.Value.Equals(valueParameter2.Value)))))
                            return false;
                }
                //if the given fact has a named parameter and the current fact hasn't then it is not more general (or equal) but less general
                //so return false
                else if (fact.Parameters[i] is NamedParameter && Parameters[i] is ValueParameter)
                    return false;
            }
            return true;
        }
    }
}