using System;
using System.Drawing;

namespace TrafficGameCore.CarModel
{
    /// <summary>
    /// Cars factory calss, creates various cars
    /// </summary>
    internal class CarsFactory
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
        internal Car CreateWhiteSlow() { return new Car(3* statisticsMultiplier, 8* statisticsMultiplier,(52,118), carsImages.CarsImagesArray[0]); }
        internal Car CreateWhiteMedium() { return new Car(4* statisticsMultiplier, 10* statisticsMultiplier, (52, 132), carsImages.CarsImagesArray[1]); }
        internal Car CreateWhiteFast() { return new Car(5* statisticsMultiplier, 10* statisticsMultiplier, (56, 134), carsImages.CarsImagesArray[3]); }
        internal Car CreateWhitePickUp() { return new Car(3* statisticsMultiplier, 8* statisticsMultiplier, (60, 138), carsImages.CarsImagesArray[5]); }
        internal Car CreateYellowTaxi() { return new Car(4* statisticsMultiplier, 10* statisticsMultiplier, (52, 118), carsImages.CarsImagesArray[2]); }
        internal Car CreateWhiteTaxi() { return new Car(4* statisticsMultiplier, 10* statisticsMultiplier, (56, 132), carsImages.CarsImagesArray[4]); }
        internal Car CreateBlackFast() { return new Car(6* statisticsMultiplier, 11* statisticsMultiplier, (56, 130), carsImages.CarsImagesArray[6]); }
        internal Car CreateRedPickUp() { return new Car(3* statisticsMultiplier, 9* statisticsMultiplier, (54, 138), carsImages.CarsImagesArray[7]); }
        internal Car CreateAmbulance() { return new Car(5* statisticsMultiplier, 9* statisticsMultiplier, (56, 142), carsImages.CarsImagesArray[8]); }
        internal Car CreatePoliceCarBlue() { return new Car(4* statisticsMultiplier, 9* statisticsMultiplier, (54, 128), carsImages.CarsImagesArray[9]); }
        internal Car CreatePoliceCarBlack() { return new Car(5* statisticsMultiplier, 9* statisticsMultiplier, (54, 128), carsImages.CarsImagesArray[10]); }
        internal Car CreatePoliceCarSecret() { return new Car(6* statisticsMultiplier, 11* statisticsMultiplier, (58, 128), carsImages.CarsImagesArray[11]); }

        /// <summary>
        /// Method creating random car
        /// </summary>
        /// <returns></returns>
        internal Car CreateRandomCar(Random random) 
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
