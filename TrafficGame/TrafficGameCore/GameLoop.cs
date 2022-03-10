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
        // temp debug
        public int NumberOfCars { get; set; } = 0;
        // Actual speed of car/game
        public double gameSpeed { get; set; } = 100;
        // Max speed of car/game
        private static double maxGameSpeed = 602;
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
                //this must be precised hex number and must sum exacly to 1
                gameSpeed += 0.0625;
            CarSpawnInterval();

            var taskStreet = Task.Factory.StartNew(()=>StreetSingleton.GetInstance().Tick(gameSpeed, gameTimeElapsed));
            var taskPlayer = Task.Factory.StartNew(() => PlayerSingleton.GetInstance().Tick(gameTimeElapsed, gameSpeed * gameTimeElapsed));
            Task.WaitAll(taskStreet, taskPlayer);
            randomCars.MoveRandomCars(gameSpeed, gameTimeElapsed);
            

            DetectCollision();
            if (collision)
                Stop();

            //temp
            threads = Process.GetCurrentProcess().Threads.Count;
        }
        private void CarSpawnInterval()
        {
            if (gameSpeed < 200)
            {
                if (gameSpeed % 30 == 0)
                    randomCars.numbersOfCars += 1;
            }
            else if (gameSpeed < 210)
            {
                randomCars.numbersOfCars = 6;
            }
            if (gameSpeed < 300)
            {
                if (gameSpeed % 50 == 0)
                    randomCars.numbersOfCars += 1;
            }
            else if (gameSpeed < 310)
            {
                randomCars.numbersOfCars = 4;
            }
            else if (gameSpeed < 500)
            {
                if (gameSpeed % 60 == 0)
                    randomCars.numbersOfCars += 1;
            }
            else
                randomCars.numbersOfCars = 6;

            NumberOfCars = randomCars.numbersOfCars;
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
            randomCars.numbersOfCars = 3;
            StreetSingleton.GetInstance().StreetImage = Properties.Resources.street;
            collision = false;
        }

        /// <summary>
        /// Funkcja wykrywająca kolizje samochodu gracza z losowymi samochodami
        /// </summary>
        private void DetectCollision()
        {
            var playersCar = PlayerSingleton.GetInstance().PlayersCar;
            foreach (Car car in randomCars.RandomCarsList)
            {
                if(playersCar.HitBox.IsCollided(car.HitBox)) 
                    collision = true;
            }
        }

    }
}
