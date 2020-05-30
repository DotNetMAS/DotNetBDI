using NetBDI.Core;
using System.Collections.Generic;

namespace NetBDI.Example2
{
    /// <summary>
    /// The environment that contains towns and goldmines
    /// </summary>
    public class GoldMineEnvironment : IEnvironment
    {
        /// <summary>
        /// The towns in the environment
        /// </summary>
        public List<Town> Towns { get; set; } = new List<Town>();

        /// <summary>
        /// The goldmines in the environment
        /// </summary>
        public List<GoldMine> GoldMines { get; set; } = new List<GoldMine>();

        /// <summary>
        /// Default constructor for the environment
        /// </summary>
        public GoldMineEnvironment()
        {
            Towns.Add(new Town(5, 5));
            Towns.Add(new Town(7, 19));
            Towns.Add(new Town(15, 9));

            GoldMines.Add(new GoldMine(1, 1));
            GoldMines.Add(new GoldMine(10, 8));
            GoldMines.Add(new GoldMine(19, 18));
            GoldMines.Add(new GoldMine(12, 15));
        }
    }
}