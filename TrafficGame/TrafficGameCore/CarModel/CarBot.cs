using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TrafficGameCore.StreetSingleton;

namespace TrafficGameCore.CarModel
{
    internal class CarBot
    {
        // List of cars
        private List<Car> _carList;

        internal CarBot(List<Car> carList)
        {
            _carList = carList;
        }
        /// <summary>
        /// Method causing car to drive
        /// </summary>
        internal void DriveCar(Car car, double gameSpeed, double gameTimeElapsed)
        {
            var playerCarSpeed = gameSpeed * gameTimeElapsed;
            if (car.Speed / 2 >= playerCarSpeed)
                car.Speed = car.Speed/2;
            car.Pos.Y += playerCarSpeed + car.Speed / 2 * (int)car.DrivingDirection;
            changeSpeed(car);
        }
        /// <summary>
        /// Method checking if there is car in front of input car so that it can slow down
        /// </summary>
        /// <param name="car"></param>
        private void changeSpeed(Car car)
        {
            foreach(var carInList in _carList)
            {
                if(car.Pos.X <= (int)Lane.Third)
                {
                    if (carInList.Pos.X == car.Pos.X && car.Pos.Y > carInList.Pos.Y - (car.HitBox.Lenght * 2))
                        car.Speed = carInList.Speed;
                }
                else
                {
                    if (carInList.Pos.X == car.Pos.X && car.Pos.Y < carInList.Pos.Y+carInList.HitBox.Lenght + (car.HitBox.Lenght))
                        car.Speed = carInList.Speed;
                }
                
            }
        }

    }
}
