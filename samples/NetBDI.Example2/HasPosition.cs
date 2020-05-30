using System;
using System.Collections.Generic;

namespace NetBDI.Example2
{
    /// <summary>
    /// An element that has a position in the environment
    /// </summary>
    public abstract class HasPosition
    {
        /// <summary>
        /// The X Position of the element
        /// </summary>
        public double X { get; protected set; }
        /// <summary>
        /// The Y position of the element
        /// </summary>
        public double Y { get; protected set; }
    }
}