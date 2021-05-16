using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrafficGameCore.CarModel
{
    /// <summary>
    /// Class representing random cars on the street
    /// </summary>
    public class RandomCars
    {
        // List of random cars
        public List<Car> RandomCarsList { get; set; }
        // Random generator
        private Random random = new Random();

        /// <summary>
        /// Constructor
        /// </summary>
        public RandomCars()
        {
            RandomCarsList = new List<Car>();
        }
        
        /// <summary>
        /// Method drawing random cars
        /// </summary>
        /// <param name="e"></param>
        public void Draw(Graphics g)
        {
            foreach(Car car in RandomCarsList)
            {
                car.Draw(g);
            }
        }

        /// <summary>
        /// Method moving random cars
        /// </summary>
        /// <param name="gameSpeed"></param>
        public async void MoveRandomCarsAsync(double gameSpeed, double gameTimeElapsed)
        {
            foreach (var car in RandomCarsList)
            {
                // todo:
                // make street lenght dependency
            }
        }

    }
}
