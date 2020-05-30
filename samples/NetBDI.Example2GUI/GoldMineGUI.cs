using NetBDI.Core;
using NetBDI.Example2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NetBDI.Example2GUI
{
    /// <summary>
    /// The user interface for our gold mine problem
    /// </summary>
    public partial class GoldMineGUI : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GoldMineGUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On showing the UI, we can start the agent in a separate thread
        /// </summary>
        /// <param name="args">The event arguments</param>
        protected override void OnShown(EventArgs args)
        {
            var t = new Thread(new ThreadStart(Start));
            t.Start();
        }

        /// <summary>
        /// We start the agent(s)
        /// </summary>
        private void Start()
        {
            //we create an environment and add two agents to that environment
            var environment = new GoldMineEnvironment();
            var agent = new GoldMineAgent(environment, 5, 5, CommitmentType.Blind);
            var agent2 = new GoldMineAgent(environment, 15, 9, CommitmentType.Blind);
            var desires = new List<Desire<GoldMineAction, GoldMineAgent, GoldMineEnvironment>> { new ExploreDesire(), new SellGoldDesire(), new MineGoldDesire() };
            var desires2 = new List<Desire<GoldMineAction, GoldMineAgent, GoldMineEnvironment>> { new ExploreDesire(), new SellGoldDesire(), new MineGoldDesire() };
            agent.Init(desires, null);
            agent2.Init(desires2, null);

            Draw(agent, agent2);
            while (true)
            {
                Thread.Sleep(1000);
                Draw(agent, agent2);
            }
        }

        /// <summary>
        /// We draw the agents
        /// </summary>
        /// <param name="agent">First agent to draw</param>
        /// <param name="agent2">Second agent to draw</param>
        private void Draw(GoldMineAgent agent, GoldMineAgent agent2)
        {
            // create bmp image
            var bmp = new Bitmap(800, 800);
            // draw on bmp image
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.Clear(Color.White);
                DrawAgent(agent, graphics);
                DrawAgent(agent2, graphics);
                var towns = agent.BeliefBase.GetBeliefSet("towns").Values;
                towns.AddRange(agent2.BeliefBase.GetBeliefSet("towns").Values);
                DrawPositions(towns, Color.Blue, graphics);

                var mines = agent.BeliefBase.GetBeliefSet("mines").Values;
                mines.AddRange(agent2.BeliefBase.GetBeliefSet("mines").Values);
                DrawPositions(mines, Color.Gold, graphics);
            }
            _pictureBox1.Image = bmp;
        }

        /// <summary>
        /// Draw an agent
        /// </summary>
        /// <param name="agent">The agent to draw</param>
        /// <param name="graphics">The graphics to draw on</param>
        private void DrawAgent(GoldMineAgent agent, Graphics graphics)
        {
            var myBrush = new SolidBrush(Color.Red);
            var rectangle = new Rectangle((int)(agent.CurrentX * 40 - 5), (int)(agent.CurrentY * 40 - 5), 10, 10);
            graphics.FillRectangle(myBrush, rectangle);
            graphics.DrawEllipse(new Pen(myBrush), new Rectangle((int)(agent.CurrentX * 40 - 80), (int)(agent.CurrentY * 40 - 80), 160, 160));
        }

        /// <summary>
        /// Draw positions
        /// </summary>
        /// <param name="objects">A list of objects to draw</param>
        /// <param name="color">The color of the object</param>
        /// <param name="graphics">The graphics to draw on</param>
        private void DrawPositions(List<object> objects, Color color, Graphics graphics)
        {
            foreach (HasPosition pos in objects)
            {
                var myBrush = new SolidBrush(color);
                var rectangle = new Rectangle((int)(pos.X * 40 - 5), (int)(pos.Y * 40 - 5), 10, 10);
                graphics.FillEllipse(myBrush, rectangle);
            }
        }
    }
}