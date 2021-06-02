using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using TrafficGameCore.HitBox;

namespace TrafficGameCore.CarModel
{
    /// <summary>
    /// Car class
    /// </summary>
    internal class Car
    {
        // Position
        public Position Pos;
        // max speed ratio
        public double MaxSpeed { get; set; }
        // Speed ratio
        public double Speed { get; set; }
        // Image from sprite sheet
        private Image CarModel;
        // Turning rate
        public int TurningRate { get; }
        // Angle of image
        public float Angle { get; set; } = 0;
        // Driving direction
        public Direction DrivingDirection { get; set; }

        // Hitbox
        public CarHitbox HitBox { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="carModel"></param>
        /// <param name="turningRate"></param>
        public Car(int speed, int turningRate, (int Width,int Lenght) size, int edgeRoundingDegree, Image carModel)
        {
            Speed = speed;
            MaxSpeed = speed;
            TurningRate = turningRate;
            CarModel = carModel;
            Pos = new Position();
            HitBox = new CarHitbox(Pos,size,edgeRoundingDegree,carModel);
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Car()
        {
            Pos = new Position();
            HitBox = new CarHitbox(Pos,(50, 140),5, new Bitmap(50, 140));
        }

        /// <summary>
        /// Car drawing method
        /// </summary>
        /// <param name="e"></param>
        public void Draw(Graphics g)
        {
            var image = RotateImage(CarModel, Angle);
            g.DrawImage(image, new RectangleF((int)Pos.X, (int)Pos.Y, image.Width, image.Height));
            //HitBox.Draw(g);
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
        /// Hamowanie
        /// </summary>
        public void Braking(double speedTarget)
        {
            if (Speed > speedTarget)
                Speed /= 1.05;
            HitBox.ChangeColor(Color.Red);
        }
        /// <summary>
        /// Przyspieszanie
        /// </summary>
        public void Accelerating()
        {
            if (Speed < MaxSpeed)
                Speed *= 1.01;
            else
                Speed = MaxSpeed;
            HitBox.ChangeColor(Color.Yellow);
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
