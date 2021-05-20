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
    public abstract class Hitbox
    {
        // List of rectangles in hitbox
        public List<HitboxRectangle> Rectangles;
        // Position of the hitbox
        protected Position Pos;

        public Hitbox()
        {
            Rectangles = new List<HitboxRectangle>();
        }
        /// <summary>
        /// Method checking if there is collision with another hitbox
        /// </summary>
        /// <param name="hitbox"></param>
        /// <returns></returns>
        public bool IsCollided(Hitbox hitbox)
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
