using NetBDI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace NetBDI.Example1
{
    /// <summary>
    /// The main entry point
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main method that starts our program
        /// </summary>
        /// <param name="args">Potential console arguments</param>
        static void Main(string[] args)
        {
            #region Scenario1
            /////SCENARIO1/////

            ////we create an environment and add an agent to that environment
            //var environment = new BlocksworldEnvironment();
            //var agent = new BlocksworldAgent(environment, CommitmentType.Blind);

            ////we create an achievable desire (a new configuration of the blocks) and initialize the agent with this desire
            //var target = new Table();
            //var yellow = new Block(Color.Yellow, target);
            //var green = new Block(Color.Green, yellow);
            //var red = new Block(Color.Red, green);
            //new Block(Color.Black, red);
            //var configureDesire1 = new ConfigureBlocksDesire(target, 1);

            ////already fulfilled desire
            //var target2 = new Table();
            //var black2 = new Block(Color.Black, target2);
            //var red2 = new Block(Color.Red, target2);
            //new Block(Color.Green, black2);
            //new Block(Color.Yellow, red2);
            //var configureDesire2 = new ConfigureBlocksDesire(target2, 2);

            ////unachievable desire
            //var target3 = new Table();
            //var black3 = new Block(Color.Black, target3);
            //var red3 = new Block(Color.Red, target3);
            //new Block(Color.Beige, black3);
            //new Block(Color.Yellow, red3);
            //var configureDesire3 = new ConfigureBlocksDesire(target3, 2);

            //agent.Init(new List<Desire<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>> { configureDesire1, configureDesire2, configureDesire3 }, null);

            //while (!Console.KeyAvailable)
            //    Thread.Sleep(1000);

            //agent.Stop();
            //Console.WriteLine("Agent stopped");
            //Thread.Sleep(5000);

            /*
            Unstack(Yellow, Red)
            PutDown(Yellow, table)
            Unstack(Green, Black)
            Stack(Green, Yellow)
            PickUp(Red, table)
            Stack(Red, Green)
            PickUp(Black, table)
            Stack(Black, Red)
            */

            #endregion

            #region Scenario2
            /////SCENARIO2/////

            ////we create an environment and add an agent to that environment
            //var environment = new BlocksworldEnvironment();
            //var agent = new BlocksworldAgent(environment, CommitmentType.Blind);

            ////we create an achievable desire (a new configuration of the blocks) and initialize the agent with this desire
            //var target = new Table();
            //var yellow = new Block(Color.Yellow, target);
            //var green = new Block(Color.Green, yellow);
            //var red = new Block(Color.Red, green);
            //new Block(Color.Black, red);
            //var configureDesire1 = new ConfigureBlocksDesire(target, 1);

            //agent.Init(new List<Desire<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>> { configureDesire1 }, null);
            //Thread.Sleep(2000);
            //var tableBl = environment.FindBlock(new Block(Color.Green, new Table()));
            //new Block(Color.Beige, tableBl);

            //while (!Console.KeyAvailable)
            //    Thread.Sleep(1000);

            //agent.Stop();
            //Console.WriteLine("Agent stopped");
            //Thread.Sleep(5000);

            /*
            Unstack(Yellow, Red)
            PutDown(Yellow, table)
            Unstack(Beige, Green)
            PutDown(Beige, table)
            Unstack(Green, Black)
            Stack(Green, Yellow)
            PickUp(Red, table)
            Stack(Red, Green)
            PickUp(Black, table)
            Stack(Black, Red)
            */

            #endregion

            #region Scenario3
            /////SCENARIO3/////

            ////we create an environment and add an agent to that environment
            //var environment = new BlocksworldEnvironment();
            //var agent = new BlocksworldAgent(environment, CommitmentType.Blind);

            ////we create an achievable desire (a new configuration of the blocks) and initialize the agent with this desire
            //var target = new Table();
            //var yellow = new Block(Color.Yellow, target);
            //var green = new Block(Color.Green, yellow);
            //var red = new Block(Color.Red, green);
            //new Block(Color.Black, red);
            //var configureDesire1 = new ConfigureBlocksDesire(target, 1);

            ////unachievable desire but becomes achievable after adding beige
            //var target3 = new Table();
            //var black3 = new Block(Color.Black, target3);
            //var red3 = new Block(Color.Red, target3);
            //new Block(Color.Beige, black3);
            //new Block(Color.Yellow, red3);
            //var configureDesire3 = new ConfigureBlocksDesire(target3, 2);

            //agent.Init(new List<Desire<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>> { configureDesire1, configureDesire3 }, null);
            //Thread.Sleep(2000);
            //var tableBl = environment.FindBlock(new Block(Color.Green, new Table()));
            //new Block(Color.Beige, tableBl);

            //while (!Console.KeyAvailable)
            //    Thread.Sleep(1000);

            //agent.Stop();
            //Console.WriteLine("Agent stopped");
            //Thread.Sleep(5000);

            /*
            Unstack(Yellow, Red)
            PutDown(Yellow, table)
            Unstack(Beige, Green)
            PutDown(Beige, table)
            Unstack(Green, Black)
            Stack(Green, Yellow)
            PickUp(Red, table)
            Stack(Red, Green)
            PickUp(Black, table)
            Stack(Black, Red)
            ---
            Unstack(Black, Red)
            PutDown(Black, table)
            Unstack(Red, Green)
            PutDown(Red, table)
            Unstack(Green, Yellow)
            PutDown(Green, table)
            PickUp(Yellow, table)
            Stack(Yellow, Red)
            PickUp(Beige, table)
            Stack(Beige, Black)
            */

            #endregion

            #region Scenario4
            /////SCENARIO4/////
            ////we create an environment and add an agent to that environment
            //var environment = new BlocksworldEnvironment();
            //var agent = new BlocksworldAgent(environment, CommitmentType.Blind);

            ////we create an achievable desire (a new configuration of the blocks) and initialize the agent with this desire
            //var target = new Table();
            //var yellow = new Block(Color.Yellow, target);
            //var green = new Block(Color.Green, yellow);
            //var red = new Block(Color.Red, green);
            //new Block(Color.Black, red);
            //var configureDesire1 = new ConfigureBlocksDesire(target, 1);

            //agent.Init(new List<Desire<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>> { configureDesire1 }, null);
            //Thread.Sleep(2000);
            ////the intention is 'achieved' as defined by the user
            //configureDesire1.Stop();

            //while (!Console.KeyAvailable)
            //    Thread.Sleep(1000);

            //agent.Stop();
            //Console.WriteLine("Agent stopped");
            //Thread.Sleep(5000);

            /*
            Unstack(Yellow, Red)
            PutDown(Yellow, table)
            Unstack(Green, Black)
            Stack(Green, Yellow)
            PickUp(Red, table)
            Stack(Red, Green)
            PickUp(Black, table)
            Stack(Black, Red)
            */

            #endregion

            #region Scenario5
            ///////SCENARIO5/////
            ////we create an environment and add an agent to that environment
            //var environment = new BlocksworldEnvironment();
            //var agent = new BlocksworldAgent(environment, CommitmentType.SingleMinded);

            ////we create an achievable desire (a new configuration of the blocks) and initialize the agent with this desire
            //var target = new Table();
            //var yellow = new Block(Color.Yellow, target);
            //var green = new Block(Color.Green, yellow);
            //var red = new Block(Color.Red, green);
            //new Block(Color.Black, red);
            //var configureDesire1 = new ConfigureBlocksDesire(target, 1);

            //agent.Init(new List<Desire<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>> { configureDesire1 }, null);
            //Thread.Sleep(2000);
            ////the intention is 'achieved' as defined by the user
            //configureDesire1.Stop();

            //while (!Console.KeyAvailable)
            //    Thread.Sleep(1000);

            //agent.Stop();
            //Console.WriteLine("Agent stopped");
            //Thread.Sleep(5000);

            /*
            Unstack(Yellow, Red)
            */

            #endregion

            #region Scenario6
            /////SCENARIO6/////
            //we create an environment and add an agent to that environment
            var environment = new BlocksworldEnvironment();
            var agent = new BlocksworldAgent(environment, CommitmentType.OpenMinded);

            //we create an achievable desire (a new configuration of the blocks) and initialize the agent with this desire
            var target = new Table();
            var yellow = new Block(Color.Yellow, target);
            var green = new Block(Color.Green, yellow);
            var red = new Block(Color.Red, green);
            new Block(Color.Black, red);
            var configureDesire1 = new ConfigureBlocksDesire(target, 1);

            agent.Init(new List<Desire<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>> { configureDesire1 }, null);

            var target2 = new Table();
            var red2 = new Block(Color.Red, target2);
            new Block(Color.Black, red2);
            var green2 = new Block(Color.Green, target2);
            new Block(Color.Yellow, green2);
            var configureDesire2 = new ConfigureBlocksDesire(target2, 5);
            Thread.Sleep(2000);
            agent.AddDesire(configureDesire2);

            while (!Console.KeyAvailable)
                Thread.Sleep(1000);

            agent.Stop();
            Console.WriteLine("Agent stopped");
            Thread.Sleep(5000);

            /*
            Unstack(Yellow, Red)
            ---New Desire
            PutDown(Yellow, table)
            Unstack(Green, Black)
            PutDown(Green, table)
            PickUp(Yellow, table)
            Stack(Yellow, Green)
            PickUp(Black, table)
            Stack(Black, Red)
            ---Old Desire
            Unstack(Yellow, Green)
            PutDown(Yellow, table)
            PickUp(Green, table)
            Stack(Green, Yellow)
            Unstack(Black, Red)
            PutDown(Black, table)
            PickUp(Red, table)
            Stack(Red, Green)
            PickUp(Black, table)
            Stack(Black, Red)
            */

            #endregion
        }
    }
}