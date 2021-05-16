using System;
using System.Drawing;
using System.Windows.Forms;
using TrafficGameCore.CarModel;

namespace TrafficGameCore
{
    /// <summary>
    /// Player class (singleton)
    /// </summary>
    public class PlayerSingleton
    {
        // Singleton instance
        private static PlayerSingleton _instance;

        // Bool variables which coresponds to pressed keys
        private bool keyUp = false, keyDown = false, keyLeft = false, keyRight = false;
        private bool keyW = false, keyS = false, keyA = false, keyD = false;

        // Player car
        public Car PlayersCar { get; set; }

        // Cars factory
        private CarsFactory carsFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        private PlayerSingleton() 
        {
            carsFactory = new CarsFactory(Properties.Resources.cars);
            PlayersCar = carsFactory.CreateBlackFast();
            PlayersCar.PosX = 300;
            PlayersCar.PosY = 550;
        }

        /// <summary>
        /// Method that returns singleton instance
        /// </summary>
        /// <returns></returns>
        public static PlayerSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PlayerSingleton();
            }
            return _instance;
        }

        /// <summary>
        /// Keys press method
        /// </summary>
        /// <param name="e"></param>
        public void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                keyA = true;
            if (e.KeyCode == Keys.D)
                keyD = true;
            if (e.KeyCode == Keys.W)
                keyW = true;
            if (e.KeyCode == Keys.S)
                keyS = true;
            if (e.KeyCode == Keys.Left)
                keyLeft = true;
            if (e.KeyCode == Keys.Right)
                keyRight = true;
            if (e.KeyCode == Keys.Up)
                keyUp = true;
            if (e.KeyCode == Keys.Down)
                keyDown = true;
        }

        /// <summary>
        /// Keys release method
        /// </summary>
        /// <param name="e"></param>
        public void KeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                keyA = false;
            if (e.KeyCode == Keys.D)
                keyD = false;
            if (e.KeyCode == Keys.W)
                keyW = false;
            if (e.KeyCode == Keys.S)
                keyS = false;
            if (e.KeyCode == Keys.Left)
                keyLeft = false;
            if (e.KeyCode == Keys.Right)
                keyRight = false;
            if (e.KeyCode == Keys.Up)
                keyUp = false;
            if (e.KeyCode == Keys.Down)
                keyDown = false;
        }

        /// <summary>
        /// Car controll update method
        /// </summary>
        public void Tick(double gameTimeElapsed, double gameSpeed)
        {
            var speed = PlayersCar.Speed * gameTimeElapsed;
            var turnSPeed = PlayersCar.TurningRate * gameTimeElapsed;
            if ((keyA || keyLeft) && PlayersCar.PosX > StreetSingleton.GetInstance().SidewalkWidth)
            {
                PlayersCar.PosX -= turnSPeed;
                PlayersCar.Angle = -5;
            }
            else
            {
                PlayersCar.Angle = 0;
            }
            if ((keyD || keyRight) && PlayersCar.PosX < StreetSingleton.GetInstance().Width - StreetSingleton.GetInstance().SidewalkWidth - PlayersCar.HitBoxWidth)
            {
                PlayersCar.PosX += turnSPeed;
                PlayersCar.Angle = 5;
            }
            if ((keyW || keyUp) && PlayersCar.PosY > 0)
            {
                PlayersCar.PosY -= speed;
            }
            if ((keyS || keyDown) && PlayersCar.PosY < StreetSingleton.GetInstance().Lenght - PlayersCar.HitBoxLenght)
            {
                if(speed>gameSpeed)
                    PlayersCar.PosY += (gameSpeed - 1);
                else
                    PlayersCar.PosY += PlayersCar.Speed* gameTimeElapsed;
            }

            if ((keyD || keyRight) && (keyA || keyLeft))
            {
                PlayersCar.Angle = 0;
            }


        }
    }
}
