using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficGameCore.HitBox
{
    /// <summary>
    /// Rectangle class
    /// </summary>
    public class HitboxRectangle
    {
        // Rectangle with position X and Y. 
        // X and Y are rectangle's top left corner coordinates
        // Width and lenght define size of the rectangle

        // Rectangle position
        public Position Pos { get; set; }
        // Rectangle size
        public (int Width, int Lenght) Size { get; set; }

        public (double X, double Y) PosCorrection {get;set;}

        //kolor
        public Color color { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        public HitboxRectangle(Position pos, (int Width, int Lenght) size, (double,double) posCorrection)
        {
            Pos = pos;
            Size = size;
            this.PosCorrection = posCorrection;
            color = Color.Yellow;
        }
        /// <summary>
        /// Method checking if there is collision with another rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public bool IsCollided(HitboxRectangle rectangle)
        {
            (double X, double Y) thisRecPos = (Pos.X + PosCorrection.X, Pos.Y + PosCorrection.Y);
            (double X, double Y) recPos = (rectangle.Pos.X + rectangle.PosCorrection.X, rectangle.Pos.Y + rectangle.PosCorrection.Y);

            if ((thisRecPos.X + Size.Width > recPos.X && thisRecPos.X < recPos.X + rectangle.Size.Width)
                && (thisRecPos.Y + Size.Lenght > recPos.Y && thisRecPos.Y < recPos.Y + rectangle.Size.Lenght))
                return true;
            else
                return false;
        }

        public void Draw(Graphics g)
        {
            (double X, double Y) pos = (Pos.X + PosCorrection.X, Pos.Y + PosCorrection.Y);
            g.FillRectangle(new SolidBrush(color), new Rectangle((int)pos.X, (int)pos.Y, Size.Width, Size.Lenght));
        }
    }
}
