using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficGameCore.HitBox
{
    /// <summary>
    /// 
    /// </summary>
    internal abstract class Hitbox
    {
        // List of rectangles in hitbox
        public List<Rectangle> Rectangles;
        // Position of the hitbox
        protected (int X, int Y) Pos;

        /// <summary>
        /// Method checking if there is collision with another hitbox
        /// </summary>
        /// <param name="hitbox"></param>
        /// <returns></returns>
        protected bool IsCollided(Hitbox hitbox)
        {
            foreach(var thisRec in Rectangles)
            {
                foreach (var rec in hitbox.Rectangles)
                {
                    if (thisRec.IsCollided(rec))
                        return true;
                } 
            }
            return false;
        }
    }
}
