using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficGameCore.HitBox
{
    internal class CarHitbox:Hitbox
    {
        public (int Width,int Lenght) Size { get; set; }
        public int EdgeRoundingDegree { get; set; }

        public CarHitbox(in (int X,int Y) pos, (int Width, int Lenght) size, int edgeRoundingDegree)
        {
            Pos = pos;
            Size = size;
            EdgeRoundingDegree = edgeRoundingDegree;
        }
    }
}
