namespace NetBDI.Core
{
    /// <summary>
    /// A simple belief of an agent
    /// </summary>
    public class Belief
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">The name of the belief</param>
        /// <param name="value">The value of the belief</param>
        public Belief(string name, object value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// The name of the belief
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The value of the belief
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Method to get the typed value of a belief
        /// </summary>
        /// <typeparam name="T">The type of the requested belief</typeparam>
        /// <returns>The typed value of this belief</returns>
        public T GetValue<T>() => (T) Value;
    }
}