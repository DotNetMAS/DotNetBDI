using NetBDI.Core;
using NetBDI.Example1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NetBDI.Example1GUI
{
    /// <summary>
    /// The User Interface for our blocksworld problem.
    /// On the left side the current situation, on the right hand side the intention (if any)
    /// </summary>
    public partial class BlocksworldGUI : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BlocksworldGUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Once we show the UI, we start the aagent in a different thread so we can draw on the UI without waiting for the agent to finish
        /// </summary>
        /// <param name="args">The event arguments (not used)</param>
        protected override void OnShown(EventArgs args)
        {
            var t = new Thread(new ThreadStart(Start));
            t.Start();
        }

        /// <summary>
        /// We initialize the agent and draw every second the situation
        /// </summary>
        private void Start()
        { 
            //we create an environment and add an agent to that environment
            var environment = new BlocksworldEnvironment();
            var agent = new BlocksworldAgent(environment, CommitmentType.Blind);

            //we create an achievable desire (a new configuration of the blocks) and initialize the agent with this desire
            var target = new Table();
            var yellow = new Block(Color.Yellow, target);
            var green = new Block(Color.Green, yellow);
            var red = new Block(Color.Red, green);
            new Block(Color.Black, red);
            var configureDesire1 = new ConfigureBlocksDesire(target, 1);

            agent.Init(new List<Desire<BlocksworldAction, BlocksworldAgent, BlocksworldEnvironment>> { configureDesire1 }, null);

            Draw(agent);
            while (true)
            {
                Thread.Sleep(1000);
                Draw(agent);
            }
        }

        /// <summary>
        /// We draw the current situation and the desire
        /// </summary>
        /// <param name="agent">The agent whose situation and environment we draw</param>
        private void Draw(BlocksworldAgent agent)
        {
            // create bmp image
            var bmp = new Bitmap(375, 400);
            // draw on bmp image
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.Clear(Color.White);
                DrawStacks(agent.Environment.Table.GetStacks(), graphics);
                if (agent.InArm != null)
                {
                    var myBrush = new SolidBrush(agent.InArm.Color);
                    var rectangle = new Rectangle(162, 0, 50, 50);
                    graphics.FillRectangle(myBrush, rectangle);
                }
            }
            _pictureBox1.Image = bmp;

            // create bmp image
            var bmp2 = new Bitmap(375, 400);
            // draw on bmp image
            using (var graphics = Graphics.FromImage(bmp2))
            {
                graphics.Clear(Color.White);
                if (agent.CurrentIntentions != null)
                {
                    var cBDesire = (ConfigureBlocksDesire)agent.CurrentIntentions.First().Desire;
                    DrawStacks(cBDesire.Target.GetStacks(), graphics);
                }
            }
            _pictureBox2.Image = bmp2;
        }

        /// <summary>
        /// We draw all stacks on a table
        /// </summary>
        /// <param name="stacks">The stacks to draw</param>
        /// <param name="graphics">The graphics to draw on</param>
        private void DrawStacks(List<List<Block>> stacks, Graphics graphics)
        {
            var left = 0;
            for (var i = 0; i < stacks.Count(); i++)
            {
                left += 50;
                var top = 400;
                var currentStack = stacks[i];
                for (var j = 0; j < currentStack.Count(); j++)
                {
                    top -= 50;
                    var myBrush = new SolidBrush(currentStack[j].Color);
                    var rectangle = new Rectangle(left, top, 50, 50);
                    graphics.FillRectangle(myBrush, rectangle);
                }
            }
        }
    }
}