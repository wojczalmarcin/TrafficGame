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
    public class RandomCarSpawner
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
        public void SpawnRandomCar(List<Car> carList)
        {
            
            var freeLanes = StreetSingleton.GetInstance().WhichLanesAreFree(carList);
            int randomPos;

            if (freeLanes.Count != 0)
            {
                randomPos = random.Next(freeLanes.Count);
                Car newCar = carsFactory.CreateRandomCar();
                newCar.PosX = (int)freeLanes[randomPos];
                newCar.PosY = -newCar.HitBoxLenght;
                if(newCar.PosX == (int)Lane.First || newCar.PosX == (int)Lane.Second)
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
