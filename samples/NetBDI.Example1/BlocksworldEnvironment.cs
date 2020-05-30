using NetBDI.Core;
using System.Drawing;

namespace NetBDI.Example1
{
    /// <summary>
    /// The environment for this blocksworld problem
    /// </summary>
    public class BlocksworldEnvironment : IEnvironment
    {
        /// <summary>
        /// An environment always contains one (and exactly one) table
        /// </summary>
        public Table Table { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BlocksworldEnvironment() { InitEnvironment(); }

        /// <summary>
        /// Find a specific block in the environment
        /// </summary>
        /// <param name="blockToFind">The block to find in the environment</param>
        /// <returns>If the block exists in the environment, we return it, otherwise we return null</returns>
        public Block FindBlock(Block blockToFind)
        {
            foreach(var block in Table.Blocks)
            {
                var foundBlock = FindBlockInBlocks(block, blockToFind);
                if (foundBlock != null)
                    return foundBlock;
            }
            return null;
        }

        /// <summary>
        /// Find a block in a certain stack
        /// </summary>
        /// <param name="block">The current block representing the base of the stack to search in</param>
        /// <param name="blockToFind">The block we wish to find</param>
        /// <returns>If the block exists in the environment, we return it, otherwise we return null</returns>
        private Block FindBlockInBlocks(Block block, Block blockToFind)
        {
            if (block.Equals(blockToFind))
                return block;
            if (block.Upper == null)
                return null;
            else
                return FindBlockInBlocks(block.Upper, blockToFind);
        }

        /// <summary>
        /// Initialize environment by stacking some blocks on the table
        /// </summary>
        private void InitEnvironment()
        {
            Table = new Table();
            var black = new Block(Color.Black, Table);
            var red = new Block(Color.Red, Table);
            new Block(Color.Yellow, red);
            new Block(Color.Green, black);
        }
    }
}