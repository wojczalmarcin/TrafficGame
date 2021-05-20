using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficGameCore.HitBox
{
    public class CarHitbox : Hitbox
    {
        public (int Width, int Lenght) Size { get; set; }
        public double EdgeRoundingDegree { get; set; }

        public CarHitbox(Position pos, (int Width, int Lenght) size, double edgeRoundingDegree, Image image)
        {
            (int Width,int Lenght) imageCorrection = ((size.Width - image.Width)/2, (size.Lenght - image.Height)/2);
            Pos = pos;
            Size = size;
            EdgeRoundingDegree = edgeRoundingDegree;
            Rectangles.Add(new HitboxRectangle(pos, (size.Width, size.Lenght - (int)edgeRoundingDegree),
                (-imageCorrection.Width, edgeRoundingDegree/2 - imageCorrection.Lenght)));
            Rectangles.Add(new HitboxRectangle(pos, (size.Width - (int)edgeRoundingDegree, size.Lenght),
                (edgeRoundingDegree / 2 - imageCorrection.Width, -imageCorrection.Lenght)));
        }
        public void Draw(Graphics g)
        {
            foreach(var rec in Rectangles)
            {
                rec.Draw(g);
            }
        }
    }
}
