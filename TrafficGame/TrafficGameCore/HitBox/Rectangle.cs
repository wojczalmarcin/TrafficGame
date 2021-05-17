using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficGameCore.HitBox
{
    /// <summary>
    /// Rectangle class
    /// </summary>
    internal class Rectangle
    {
        // Rectangle with position X and Y. 
        // X and Y are rectangle's top left corner coordinates
        // Width and lenght define size of the rectangle

        // Rectangle position
        private (int X, int Y) Pos;
        // Rectangle size
        public (int Width, int Lenght) Size { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        public Rectangle(in (int X, int Y) pos, (int Width, int Lenght) size)
        {
            Pos = pos;
            Size = size;
        }
        /// <summary>
        /// Method checking if there is collision with another rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public bool IsCollided(Rectangle rectangle)
        {

            if ((Pos.X + Size.Width > rectangle.Pos.X && Pos.X < rectangle.Pos.X + rectangle.Size.Width)
                && (Pos.Y + Size.Lenght > rectangle.Pos.Y && Pos.Y < rectangle.Pos.Y + rectangle.Size.Lenght))
                return true;
            else
                return false;
        }
    }
}
