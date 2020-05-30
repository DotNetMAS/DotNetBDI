namespace NetBDI.STRIPS
{
    /// <summary>
    /// A named parameter which is not instantiated yet.
    /// This parameter simply contains a name so it can be referenced and substituted over a number of facts by its name
    /// </summary>
    public class NamedParameter : IParameter
    {
        /// <summary>
        /// The name of the parameter. This must be unique over all parameters in an action
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The initial name of a parameter</param>
        public NamedParameter(string name)
        {
            Name = name;
        }

        /// <summary>
        /// An override of the ToString() function to be able to print to the console actions or facts
        /// </summary>
        /// <returns>The name of the parameter</returns>
        public override string ToString() => Name;
    }
}