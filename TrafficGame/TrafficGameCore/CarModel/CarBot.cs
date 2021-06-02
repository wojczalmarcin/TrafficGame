using System;
using System.Collections.Generic;
using System.Drawing;
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

            car.Pos.Y += playerCarSpeed + car.Speed * (int)car.DrivingDirection;


            if (AvoidPlayer(car))
            {
                car.Braking(1);
            }
            else
            {
                var braking = ShouldIBrake(car);
                if (braking.Item1)
                    car.Braking(braking.Item2);
                else
                    car.Accelerating();
            }
        }
        /// <summary>
        /// Method checking if there is car in front of input car so that it can slow down
        /// </summary>
        /// <param name="car"></param>
        private (bool,double) ShouldIBrake(Car car)
        {
            foreach(var carInList in _carList)
            {
                // walidacja żeby nie porównywać samochodu z samym sobą
                if (car.Pos != carInList.Pos)
                {
                    if (car.Pos.X <= (int)Lane.Third)
                    {
                        if (carInList.Pos.X == car.Pos.X &&
                            (car.Pos.Y + car.HitBox.Size.Lenght) > (carInList.Pos.Y - (car.HitBox.Size.Lenght * 2.5))
                            && (car.Pos.Y + car.HitBox.Size.Lenght) <= carInList.Pos.Y)
                        {
                            return (true, carInList.Speed);
                        }

                    }
                    else
                    {
                        if (carInList.Pos.X == car.Pos.X
                            && (car.Pos.Y < (carInList.Pos.Y + carInList.HitBox.Size.Lenght + (car.HitBox.Size.Lenght*2.5)))
                            && (car.Pos.Y >= carInList.Pos.Y + carInList.HitBox.Size.Lenght))
                        {
                            return (true, carInList.Speed);
                        }

                    }
                }
            }
            return (false, 0);
        }
        private bool AvoidPlayer(Car car)
        {
            var playerCar = PlayerSingleton.GetInstance().PlayersCar;
            if (car.Pos.X <= (int)Lane.Third)
            {
                if (playerCar.Pos.X + playerCar.HitBox.Size.Width > car.Pos.X && playerCar.Pos.X < car.Pos.X + car.HitBox.Size.Width)
                {
                    var distance = car.Pos.Y + car.HitBox.Size.Lenght - playerCar.Pos.Y;
                    if (distance < 0 && distance > -playerCar.HitBox.Size.Lenght * 3)
                        return true;

                }
                    
            }
            return false;
        }
    }
}
