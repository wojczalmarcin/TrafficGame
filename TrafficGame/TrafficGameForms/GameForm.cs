using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // Initialize Paint Event
            Paint += GameForm_Paint;
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
