using System;
using System.Diagnostics;
using System.Windows.Forms;
using TrafficGameCore;

namespace TrafficGameForms
{
    public partial class GameForm : Form
    {
        Timer graphicsTimer;
        GameLoop gameLoop;
        int frameRate = 120;
        bool gameFinished=false;
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
            var watch = Stopwatch.StartNew();
            pictureBoxGraphics.Refresh();
            labelDebugger.Text = "Threads:\n" + gameLoop.threads +"\nGameSpeed:\n"+ (int)gameLoop.gameSpeed
                + "\nCarsNumber:\n" + gameLoop.NumberOfCars;
            if (!gameLoop.Running)
            {
                gameFinished = true;
                graphicsTimer.Stop();
                MessageBox.Show("Przegrałeś!");
            }
                
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
