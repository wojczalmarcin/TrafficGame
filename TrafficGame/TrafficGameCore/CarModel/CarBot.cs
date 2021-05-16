using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TrafficGameCore.StreetSingleton;

namespace TrafficGameCore.CarModel
{
    public class CarBot
    {
        // List of cars
        private List<Car> _carList;

        public CarBot(List<Car> carList)
        {
            _carList = carList;
        }
        /// <summary>
        /// Method causing car to drive
        /// </summary>
        public void DriveCar(Car car, double gameSpeed, double gameTimeElapsed)
        {
            var playerCarSpeed = gameSpeed * gameTimeElapsed;
            if (car.Speed / 10 >= playerCarSpeed)
                car.PosY += playerCarSpeed + 2 * (int)car.DrivingDirection;
            else
                car.PosY += playerCarSpeed + car.Speed / 10 * (int)car.DrivingDirection;
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
                if(car.PosX <= (int)Lane.Third)
                {
                    if (carInList.PosX == car.PosX && car.PosY > carInList.PosY - (car.HitBoxLenght * 2))
                        car.Speed = carInList.Speed;
                }
                else
                {
                    if (carInList.PosX == car.PosX && car.PosY < carInList.PosY+carInList.HitBoxLenght + (car.HitBoxLenght))
                        car.Speed = carInList.Speed;
                }
                
            }
        }

    }
}
