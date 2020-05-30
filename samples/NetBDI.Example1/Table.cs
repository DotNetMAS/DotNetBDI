using System.Collections.Generic;
using System.Drawing;

namespace NetBDI.Example1
{
    /// <summary>
    /// A table in our environment. We assume in this example that there is only table
    /// </summary>
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Table : IElement
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        /// <summary>
        /// A list of blocks touching the table (being placed directly on the table)
        /// </summary>
        public List<Block> Blocks { get; set; } = new List<Block>();

        /// <summary>
        /// The color of the table
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Add a block on the table
        /// </summary>
        /// <param name="block">The block to be added to the table</param>
        public void AddBlock(Block block)
        {
            Blocks.Add(block);
        }

        /// <summary>
        /// Remove a block from the table
        /// </summary>
        /// <param name="block">The block to be removed from the table</param>
        public void RemoveBlock(Block block)
        {
            Blocks.Remove(block);
        }

        /// <summary>
        /// Override of the equals function to determine equality of facts
        /// </summary>
        /// <param name="obj">The object to check for equality with this table</param>
        /// <returns>True if the given object is a table, false otherwise</returns>
        public override bool Equals(object obj) =>obj is Table; //there is only one table

        /// <summary>
        /// Gets the stacks of all blocks on this table
        /// </summary>
        /// <returns>A stack containing a list of a list of blocks. A list of blocks is a stack</returns>
        public List<List<Block>> GetStacks()
        {
            var lst = new List<List<Block>>();
            foreach (var block in Blocks)
                lst.Add(block.GetUpperStack());

            return lst;
        }
    }
}