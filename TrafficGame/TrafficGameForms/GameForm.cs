using System;
using System.Windows.Forms;
using TrafficGameCore;

namespace TrafficGameForms
{
    public partial class GameForm : Form
    {
        Timer graphicsTimer;
        GameLoop gameLoop;
        int frameRate = 120;
        public GameForm()
        {
            InitializeComponent();
            // Initialize graphicsTimer
            graphicsTimer = new Timer();
            graphicsTimer.Interval = 1000 / frameRate;
            graphicsTimer.Tick += GraphicsTimer_Tick;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            gameLoop = new GameLoop();
            gameLoop.Load();
            gameLoop.Start();

            // Start Graphics Timer
            graphicsTimer.Start();
        }

        private void GraphicsTimer_Tick(object sender, EventArgs e)
        {
            pictureBoxGraphics.Refresh();
            labelDebugger.Text = "Threads:\n" + gameLoop.threads;
        }
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            PlayerSingleton.GetInstance().KeyDown(e);
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            PlayerSingleton.GetInstance().KeyUp(e);
        }

        private void pictureBoxGraphics_Paint(object sender, PaintEventArgs e)
        {
            gameLoop.Draw(e.Graphics);
        }
    }
}
