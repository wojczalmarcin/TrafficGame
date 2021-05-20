using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TrafficGameCore.StreetSingleton;

namespace TrafficGameCore.CarModel
{
    /// <summary>
    /// Class which is resposible of putting random car on the street
    /// </summary>
    internal class RandomCarSpawner
    {
        // Cars factory
        private CarsFactory carsFactory;
        // Random generator
        private Random random = new Random();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="spriteSheet"></param>
        public RandomCarSpawner(Image spriteSheet)
        {
            carsFactory = new CarsFactory(spriteSheet);
        }
        /// <summary>
        /// Method spawning cars on the specific lane
        /// </summary>
        /// <param name="carList"></param>
        internal void SpawnRandomCar(List<Car> carList)
        {
            
            var freeLanes = StreetSingleton.GetInstance().WhichLanesAreFree(carList);
            int randomPos;

            if (freeLanes.Count != 0)
            {
                randomPos = random.Next(freeLanes.Count);
                Car newCar = carsFactory.CreateRandomCar(random);
                newCar.Pos.X = (int)freeLanes[randomPos];
                newCar.Pos.Y = -newCar.HitBox.Size.Lenght;
                if(newCar.Pos.X == (int)Lane.First || newCar.Pos.X == (int)Lane.Second || newCar.Pos.X == (int)Lane.Third)
                {
                    newCar.DrivingDirection = Car.Direction.Bottom;
                    newCar.Angle = 180;
                }
                else
                {
                    newCar.DrivingDirection = Car.Direction.Top;
                    newCar.Angle = 0;
                }

                carList.Add(newCar);
            }

        }
    }
}
