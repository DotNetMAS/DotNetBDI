using System.Drawing;

namespace NetBDI.Example1
{
    /// <summary>
    /// An element in the environment
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// Each element must have a color
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Each element should be able to accept a block on itself
        /// </summary>
        /// <param name="block">The block to be accepted on itself</param>
        public void AddBlock(Block block);

        /// <summary>
        /// Each element should be able to remove a block that is stacked on itself
        /// </summary>
        /// <param name="block">The block to remove</param>
        public void RemoveBlock(Block block);
    }
}