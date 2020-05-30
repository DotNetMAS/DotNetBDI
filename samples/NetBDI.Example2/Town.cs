namespace NetBDI.Example2
{
    /// <summary>
    /// A town in the environment
    /// </summary>
    public class Town : HasPosition
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="x">The x-position of the town</param>
        /// <param name="y">The y-position of the town</param>
        public Town(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}