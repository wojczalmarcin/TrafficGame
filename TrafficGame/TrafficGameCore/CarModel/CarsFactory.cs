using System;
using System.Drawing;

namespace TrafficGameCore.CarModel
{
    /// <summary>
    /// Cars factory calss, creates various cars
    /// </summary>
    public class CarsFactory
    {
        // Instance of carsImages which is resposible of creating cutting cars from sprite sheet
        private CarsImages carsImages;
        // Random generator
        private Random random = new Random();
        // Multiplier created to easier game dificulty controll
        private static int statisticsMultiplier = 20;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="spriteSheet"></param>
        public CarsFactory(Image spriteSheet)
        {
            carsImages = new CarsImages(spriteSheet);
        }
        public Car CreateWhiteSlow() { return new Car(3* statisticsMultiplier, 6* statisticsMultiplier, carsImages.CarsImagesArray[0]); }
        public Car CreateWhiteMedium() { return new Car(4* statisticsMultiplier, 8* statisticsMultiplier, carsImages.CarsImagesArray[1]); }
        public Car CreateWhiteFast() { return new Car(5* statisticsMultiplier, 8* statisticsMultiplier, carsImages.CarsImagesArray[3]); }
        public Car CreateWhitePickUp() { return new Car(3* statisticsMultiplier, 6* statisticsMultiplier, carsImages.CarsImagesArray[5]); }
        public Car CreateYellowTaxi() { return new Car(4* statisticsMultiplier, 8* statisticsMultiplier, carsImages.CarsImagesArray[2]); }
        public Car CreateWhiteTaxi() { return new Car(4* statisticsMultiplier, 8* statisticsMultiplier, carsImages.CarsImagesArray[4]); }
        public Car CreateBlackFast() { return new Car(6* statisticsMultiplier, 9* statisticsMultiplier, carsImages.CarsImagesArray[6]); }
        public Car CreateRedPickUp() { return new Car(3* statisticsMultiplier, 7* statisticsMultiplier, carsImages.CarsImagesArray[7]); }
        public Car CreateAmbulance() { return new Car(5* statisticsMultiplier, 7* statisticsMultiplier, carsImages.CarsImagesArray[8]); }
        public Car CreatePoliceCarBlue() { return new Car(4* statisticsMultiplier, 7* statisticsMultiplier, carsImages.CarsImagesArray[9]); }
        public Car CreatePoliceCarBlack() { return new Car(5* statisticsMultiplier, 7* statisticsMultiplier, carsImages.CarsImagesArray[10]); }
        public Car CreatePoliceCarSecret() { return new Car(6* statisticsMultiplier, 9* statisticsMultiplier, carsImages.CarsImagesArray[11]); }

        /// <summary>
        /// Method creating random car
        /// </summary>
        /// <returns></returns>
        public Car CreateRandomCar() 
        {

            switch (random.Next(12))
            {
                case 0:
                    return CreateWhiteSlow();
                case 1:
                    return CreateWhiteMedium();
                case 2:
                    return CreateWhiteFast();
                case 3:
                    return CreateWhitePickUp();
                case 4:
                    return CreateYellowTaxi();
                case 5:
                    return CreateWhiteTaxi();
                case 6:
                    return CreateBlackFast();
                case 7:
                    return CreateRedPickUp();
                case 8:
                    return CreateAmbulance();
                case 9:
                    return CreatePoliceCarBlue();
                case 10:
                    return CreatePoliceCarBlack();
                default:
                    return CreatePoliceCarSecret();
            }
        }
    }
}
