using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrafficGameCore
{
    /// <summary>
    /// Core of the game
    /// </summary>
    public class GameLoop
    {
        // Actual speed of car/game
        private double gameSpeed = 100;
        // Max speed of car/game
        private static double maxGameSpeed = 300;
        // Gameloop status
        public bool Running { get; private set; }

        // Gameloop start method
        public async Task Start()
        {
            // Set gameloop state
            Running = true;

            // Set previous game time
            DateTime _previousGameTime = DateTime.Now;

            while (Running)
            {
                // Calculate the time elapsed since the last game loop cycle
                TimeSpan GameTime = DateTime.Now - _previousGameTime;
                // Update the current previous game time
                _previousGameTime = _previousGameTime + GameTime;
                // Update the game
                double gameTimeElapsed = GameTime.TotalMilliseconds / 1000;
                Tick(gameTimeElapsed);
                // Update Game at 60fps
                await Task.Delay(8);
            }
        }
        /// <summary>
        /// Stop GameLoop
        /// </summary>
        public void Stop()
        {
            Running = false;
        }

        /// <summary>
        /// Update game logic
        /// </summary>
        /// <param name="gameTimeElapsed"></param>
        private void Tick(double gameTimeElapsed)
        {
            if(gameSpeed<maxGameSpeed)
                gameSpeed += 0.01;
            StreetSingleton.GetInstance().Tick(gameSpeed, gameTimeElapsed);
            PlayerSingleton.GetInstance().Tick(gameTimeElapsed, gameSpeed * gameTimeElapsed);
        }

        /// <summary>
        /// Draw game
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            StreetSingleton.GetInstance().Draw(g);
            PlayerSingleton.GetInstance().PlayersCar.Draw(g);
        }

        /// <summary>
        /// Load images
        /// </summary>
        public void Load()
        {
            StreetSingleton.GetInstance().StreetImage = Properties.Resources.street;
        }

    }
}
