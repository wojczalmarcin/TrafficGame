using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrafficGameCore.CarModel;
using TrafficGameCore.HitBox;

namespace TrafficGameCore
{
    /// <summary>
    /// Class representing street on which cars are moving
    /// </summary>
    public class StreetSingleton
    {
        internal class TireTrack
        {
            private CarHitbox _carHitBox;
            internal TireTrack(CarHitbox carHitBox)
            {
                _carHitBox = carHitBox;
                Pos = new Position(carHitBox.Rectangles[0].Pos.X + carHitBox.Rectangles[0].PosCorrection.X, carHitBox.Rectangles[0].Pos.Y+50);
            }
            public Position Pos { get; set; }
            public double Lenght { get; set; }
            private Color color = Color.Black;
            
            public void Draw(Graphics g)
            {
                g.FillRectangle(new SolidBrush(color), new Rectangle((int)Pos.X+3, (int)Pos.Y, 6, 2));
                g.FillRectangle(new SolidBrush(color), new Rectangle((int)Pos.X-6+ _carHitBox.Size.Width, (int)Pos.Y, 6, 2));
            }
        }

        // Singleton instance
        private static StreetSingleton _instance;

        // street position on Y
        public int PosY { get; set; }

        // Lenght of street 
        public int Lenght { get; set; }

        // Width of street 
        public int Width { get; set; }

        // Width of street 
        public int SidewalkWidth { get; set; }

        // Image of the street
        public Image StreetImage { get; set; }

        // List of tire tracks
        internal List<TireTrack> TireTracks { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="randomCars"></param>
        private StreetSingleton()
        {
            Width = 700;
            Lenght = 800;
            SidewalkWidth = 60;
            TireTracks = new List<TireTrack>();
        }
        /// <summary>
        /// Method returning instance of street object
        /// </summary>
        /// <returns></returns>
        public static StreetSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StreetSingleton();
            }
            return _instance;
        }

        /// <summary>
        /// Enum representing street lanes and car position on specified lane
        /// </summary>
        public enum Lane
        {
            First = 75,
            Second = 170,
            Third = 270,
            Fourth = 370,
            Fifth = 460,
            Sixth = 560
        }
        /// <summary>
        /// Method that returns lanes on which car can be spawned
        /// </summary>
        /// <param name="carIndex"></param>
        /// <returns></returns>
        internal List<Lane> WhichLanesAreFree(List<Car> carList)
        {
            var freeLanes = new List<Lane>();
            foreach (Lane lane in (Lane[])Enum.GetValues(typeof(Lane)))
                freeLanes.Add(lane);

            var carsCount = carList.Count();

            foreach (var car in carList)
            {
                if (car.Pos.Y <= car.HitBox.Size.Lenght)
                    freeLanes.Remove((Lane)car.Pos.X);
            }

            if (carsCount >= 5)
            {
                if (freeLanes.Count == 1)
                    freeLanes.Clear();
                else if (freeLanes.Count == 2)
                {
                    Parallel.ForEach(carList, car =>
                    {
                        if (car.Pos.Y <= car.HitBox.Size.Lenght * 2 + 50)
                        {
                            if (freeLanes.Count == 2)
                            {
                                if (car.Pos.X == (int)freeLanes[0] || car.Pos.X == (int)freeLanes[1])
                                    freeLanes.Remove((Lane)car.Pos.X);
                            }
                            else if (freeLanes.Count == 1)
                            {
                                if (car.Pos.X == (int)freeLanes[0])
                                    freeLanes.Remove((Lane)car.Pos.X);
                            }
                        }
                    });
                }
                if (freeLanes.Count == 1)
                    freeLanes.Clear();
            }

            return freeLanes;
        }
        private void RemoveTireTracks()
        {
            var tracksToRemove = new List<TireTrack>();
            foreach(var tireTrack in TireTracks)
            {
                if (tireTrack.Pos.Y > 1024)
                    tracksToRemove.Add(tireTrack);
            }
            foreach(var track in tracksToRemove)
            {
                TireTracks.Remove(track);
            }
        }
        private void MoveTireTracks(int y)
        {
            foreach (var tireTrack in TireTracks)
                tireTrack.Pos.Y += y;
        }
        /// <summary>
        /// Function moving street
        /// </summary>
        /// <param name="gameSpeed"></param>
        public void Tick(double gameSpeed, double gameTimeElapsed)
        {
            PosY += (int)(gameSpeed*gameTimeElapsed);
            if (PosY >= 1024)
                PosY = 0;
            RemoveTireTracks();
            MoveTireTracks((int)(gameSpeed * gameTimeElapsed));
        }

        /// <summary>
        /// Function that draws street
        /// </summary>
        /// <param name="e"></param>
        public void Draw(Graphics g)
        {
            g.DrawImage(StreetImage, new RectangleF(0, (int)PosY, Properties.Resources.street.Width, Properties.Resources.street.Height));
            g.DrawImage(StreetImage, new RectangleF(0, (int)PosY - Properties.Resources.street.Height, Properties.Resources.street.Width, Properties.Resources.street.Height));
            foreach (var tireTrack in TireTracks)
                tireTrack.Draw(g);
        }
    }
}
