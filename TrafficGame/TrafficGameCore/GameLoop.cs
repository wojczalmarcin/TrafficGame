using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrafficGameCore.CarModel;

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
        private static double maxGameSpeed = 402;
        // Gameloop status
        public bool Running { get; private set; }
        // Random cars
        private RandomCars randomCars;
        // Sprite sheet
        Image spriteSheet;
        private bool collision;
        //temp
        public int threads { get; set; }

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
                gameSpeed += 1;
            if (gameSpeed % 50 == 25)
                randomCars.numbersOfCars += 1;
                
            StreetSingleton.GetInstance().Tick(gameSpeed, gameTimeElapsed);
            PlayerSingleton.GetInstance().Tick(gameTimeElapsed, gameSpeed * gameTimeElapsed);
            randomCars.MoveRandomCars(gameSpeed, gameTimeElapsed);
            detectCollision();
            if (collision)
                Stop();

            //temp
            threads = Process.GetCurrentProcess().Threads.Count;
        }

        /// <summary>
        /// Draw game
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            StreetSingleton.GetInstance().Draw(g);
            PlayerSingleton.GetInstance().PlayersCar.Draw(g);
            randomCars.Draw(g);
        }

        /// <summary>
        /// Load images etc.
        /// </summary>
        public void Load()
        {
            spriteSheet = Properties.Resources.cars;
            randomCars = new RandomCars(spriteSheet);
            randomCars.RandomCarsList.Clear();
            randomCars.numbersOfCars = 1;
            StreetSingleton.GetInstance().StreetImage = Properties.Resources.street;
            collision = false;
        }

        /// <summary>
        /// Funkcja wykrywająca kolizje samochodu gracza z losowymi samochodami
        /// </summary>
        private void detectCollision()
        {
            var playersCar = PlayerSingleton.GetInstance().PlayersCar;
            foreach (Car car in randomCars.RandomCarsList)
            {
                if ((playersCar.Pos.X + playersCar.HitBox.Width > car.Pos.X && playersCar.Pos.X < car.Pos.X + car.HitBox.Width)
                   && (playersCar.Pos.Y + playersCar.HitBox.Lenght > car.Pos.Y && playersCar.Pos.Y < car.Pos.Y+ car.HitBox.Lenght))
                {
                    collision = true;
                }
            }
        }

    }
}
