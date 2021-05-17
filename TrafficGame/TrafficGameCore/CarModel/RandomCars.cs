using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[assembly: InternalsVisibleToAttribute("TrafficGameNUnitTest")]

namespace TrafficGameCore.CarModel
{
    /// <summary>
    /// Class representing random cars on the street
    /// </summary>
    internal class RandomCars
    {
        // List of random cars
        internal List<Car> RandomCarsList { get; set; }
        // Random generator
        private Random random = new Random();
        // Number of cars
        public int numbersOfCars { get; set; }
        // RandomCarSpawner instance
        private RandomCarSpawner randomCarSpawner;
        // CarBot instance
        private CarBot carBot;

        /// <summary>
        /// Constructor
        /// </summary>
        public RandomCars(Image spriteSheet)
        {
            numbersOfCars = 0;
            RandomCarsList = new List<Car>();
            randomCarSpawner = new RandomCarSpawner(spriteSheet);
            carBot = new CarBot(RandomCarsList);
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
        public void MoveRandomCars(double gameSpeed, double gameTimeElapsed)
        {
            var carsToRemove = new List<Car>();
            if (RandomCarsList.Count != numbersOfCars)
                randomCarSpawner.SpawnRandomCar(RandomCarsList);
            foreach (var car in RandomCarsList)
            {
                carBot.DriveCar(car, gameSpeed, gameTimeElapsed);
                if (car.Pos.Y > StreetSingleton.GetInstance().Lenght)
                    carsToRemove.Add(car);
            }
            foreach(var car in carsToRemove)
            {
                RandomCarsList.Remove(car);
            }
        }

    }
}
