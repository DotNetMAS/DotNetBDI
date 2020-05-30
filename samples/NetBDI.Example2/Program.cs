using NetBDI.Core;
using System;
using System.Collections.Generic;
using System.Threading;

namespace NetBDI.Example2
{
    /// <summary>
    /// The main entry point
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main method that starts our program
        /// </summary>
        static void Main(string[] args)
        {
            #region Scenario1
            /////SCENARIO1/////

            //we create an environment and add an agent to that environment
            var environment = new GoldMineEnvironment();
            var agent = new GoldMineAgent(environment, 5, 5, CommitmentType.Blind);
            agent.Init(new List<Desire<GoldMineAction, GoldMineAgent, GoldMineEnvironment>> { new ExploreDesire(), new SellGoldDesire(), new MineGoldDesire() }, null);

            while (!Console.KeyAvailable)
                Thread.Sleep(1000);

            #endregion
        }
    }
}