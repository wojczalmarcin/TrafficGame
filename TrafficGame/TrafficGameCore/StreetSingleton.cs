using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrafficGameCore.CarModel;

namespace TrafficGameCore
{
    /// <summary>
    /// Class representing street on which cars are moving
    /// </summary>
    public class StreetSingleton
    {
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


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="randomCars"></param>
        private StreetSingleton()
        {
            Width = 700;
            Lenght = 800;
            SidewalkWidth = 60;
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
            First = 70,
            Second = 170,
            Third = 270,
            Fourth = 370,
            Fifth = 470,
            Sixth = 570
        }
        /// <summary>
        /// Method that returns lanes on which car can be spawned
        /// </summary>
        /// <param name="carIndex"></param>
        /// <returns></returns>
        public List<Lane> WhichLanesAreFree(List<Car> carList)
        {
            var freeLanes = new List<Lane>();
            foreach (Lane lane in (Lane[])Enum.GetValues(typeof(Lane)))
                freeLanes.Add(lane);

            var carsCount = carList.Count();

            foreach (var car in carList)
            {
                if (car.Pos.Y <= car.HitBoxLenght)
                    freeLanes.Remove((Lane)car.Pos.X);
            }

            if (carsCount >= 5) {
                if (freeLanes.Count == 1)
                    freeLanes.Clear();
                else if (freeLanes.Count == 2)
                {
                    Parallel.ForEach(carList, car =>
                    {
                        if (car.Pos.Y <= car.HitBoxLenght * 2 + 50)
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

        /// <summary>
        /// Function moving street
        /// </summary>
        /// <param name="gameSpeed"></param>
        public void Tick(double gameSpeed, double gameTimeElapsed)
        {
            PosY += (int)(gameSpeed*gameTimeElapsed);
            if (PosY >= 1024)
                PosY = 0;
        }

        /// <summary>
        /// Function that draws street
        /// </summary>
        /// <param name="e"></param>
        public void Draw(Graphics g)
        {
            g.DrawImage(StreetImage, new RectangleF(0, (int)PosY, Properties.Resources.street.Width, Properties.Resources.street.Height));
            g.DrawImage(StreetImage, new RectangleF(0, (int)PosY - Properties.Resources.street.Height, Properties.Resources.street.Width, Properties.Resources.street.Height));
        }
    }
}
