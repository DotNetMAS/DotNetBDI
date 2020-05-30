namespace NetBDI.Example2
{
    /// <summary>
    /// A gold mine in our environment
    /// </summary>
    public class GoldMine : HasPosition
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="x">The x position of our goldmine</param>
        /// <param name="y">The y position of our goldmine</param>
        public GoldMine(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}