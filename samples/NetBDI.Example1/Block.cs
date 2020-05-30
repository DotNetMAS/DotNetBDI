using System.Collections.Generic;
using System.Drawing;

namespace NetBDI.Example1
{
    /// <summary>
    /// A representation of a block in our environment
    /// </summary>
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Block : IElement
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        /// The element that is below the block. This can be a table or another block.
        /// A block must always have a lower element as it can not float in the air ;-)
        /// We also assume that a block can only be placed on one (and not multiple) elements
        /// </summary>
        public IElement Lower { get; set; }

        /// <summary>
        /// The color of this block
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// (Optionally) A block that is stacked on this block. We again assume that only one block can be placed on another block
        /// </summary>
        public Block Upper { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="color">The color of this block</param>
        /// <param name="lower">The element this block is placed upon.</param>
        public Block(Color color, IElement lower)
        {
            Color = color;
            lower.AddBlock(this);
            Lower = lower;
        }

        /// <summary>
        /// Adds another block on this block
        /// </summary>
        /// <param name="block">The block to stack on this block</param>
        public void AddBlock(Block block)
        {
            Upper = block;
        }

        /// <summary>
        /// Remove block from this block
        /// </summary>
        /// <param name="block">The block to remove from this block</param>
        public void RemoveBlock(Block block)
        {
            if (Upper == block)
                Upper = null;
        }

        /// <summary>
        /// Override of the toString method for proper reporting
        /// </summary>
        /// <returns>A string representation of this block</returns>
        public override string ToString() => Color.Name;

        /// <summary>
        /// Override of the equals method for proper comparison
        /// </summary>
        /// <param name="obj">The object to compare with</param>
        /// <returns>True if the blocks have the same color</returns>
        public override bool Equals(object obj) => (obj is Block otherBlock && otherBlock.Color == Color);

        /// <summary>
        /// Get blocks of this stack above the current block including the block itself
        /// </summary>
        /// <returns>A stack of blocks in list form</returns>
        public List<Block> GetUpperStack()
        {
            var lst = new List<Block> { this };
            if (Upper != null)
                lst.AddRange(Upper.GetUpperStack());

            return lst;
        }
    }
}