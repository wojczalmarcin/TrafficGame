using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TrafficGameCore.CarModel
{
    /// <summary>
    /// Car class
    /// </summary>
    public class Car
    {
        // X position
        public double PosX { get; set; }
        // Y position
        public double PosY { get; set; }
        // Speed ratio
        public int Speed { get; }
        // Image from sprite sheet
        private Image CarModel;
        // Turning rate
        public int TurningRate { get; }
        // Angle of image
        public float Angle { get; set; } = 0;
        // Driving direction
        public Direction DrivingDirection { get; set; }
        // Hitbox lenght
        public float HitBoxLenght { get; set; } = 145;
        // Hitbox width
        public float HitBoxWidth { get; set; } = 70;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="carModel"></param>
        /// <param name="turningRate"></param>
        public Car(int speed, int turningRate, Image carModel)
        {
            Speed = speed;
            TurningRate = turningRate;
            CarModel = carModel;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Car()
        {
        }

        /// <summary>
        /// Method causing car to drive straight
        /// </summary>
        public void DriveStraight(double gameSpeed, double gameTimeElapsed)
        {
            var playerCarSpeed = gameSpeed * gameTimeElapsed;
            if(Speed/10>=playerCarSpeed)
                PosY += playerCarSpeed + 2 * (int)DrivingDirection;
            else
                PosY += playerCarSpeed + Speed/10 * (int)DrivingDirection;
        }
        /// <summary>
        /// Car drawing method
        /// </summary>
        /// <param name="e"></param>
        public void Draw(Graphics g)
        {
            var image = RotateImage(CarModel, Angle);
            g.DrawImage(image, new RectangleF((int)PosX, (int)PosY, image.Width, image.Height));
        }

        /// <summary>
        /// Auxiliary method to rotate car's image
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        private static Image RotateImage(Image img, float rotationAngle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            gfx.RotateTransform(rotationAngle);
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(img, new Point(0, 0));
            gfx.Dispose();
            return bmp;
        }

        /// <summary>
        /// Driving direction enum
        /// </summary>
        public enum Direction
        {
            Top = -1,
            Bottom = 1
        }
    }
}
