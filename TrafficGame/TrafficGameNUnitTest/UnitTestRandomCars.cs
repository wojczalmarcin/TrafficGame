using NUnit.Framework;
using TrafficGameCore;
using TrafficGameCore.CarModel;
using System.Drawing;
using static TrafficGameCore.StreetSingleton;

namespace TrafficGameNUnitTest
{
    [TestFixture]
    public class Street_WhichLanesAreFree
    {
        private RandomCars _randomCars;

        [SetUp]
        public void Setup()
        {
            _randomCars = new RandomCars(new Bitmap(700, 1024));
        }

        [TestCase(Lane.First, 0)]
        [TestCase(Lane.Second, 0)]
        [TestCase(Lane.Third, 0)]
        [TestCase(Lane.Fourth, 0)]
        [TestCase(Lane.Fifth, 0)]
        [TestCase(Lane.Sixth, 0)]
        [TestCase(Lane.First, 140)]
        [TestCase(Lane.Second, 140)]
        [TestCase(Lane.Third, 140)]
        [TestCase(Lane.Fourth, 140)]
        [TestCase(Lane.Fifth, 140)]
        [TestCase(Lane.Sixth, 140)]
        public void WhichLanesAreFree_HowManyLanesAreFreeWithOneCar(int x, int y)
        {
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList[0].Pos.X = x;
            _randomCars.RandomCarsList[0].Pos.Y = y;
            var freeLanes = StreetSingleton.GetInstance().WhichLanesAreFree(_randomCars.RandomCarsList);

            Assert.AreEqual(5, freeLanes.Count);
        }

        [TestCase((int)Lane.First, 0, (int)Lane.Second, 0, (int)Lane.Third, 0)]
        [TestCase((int)Lane.First, 0, (int)Lane.Second, 0, (int)Lane.Fourth, 0)]
        [TestCase((int)Lane.First, 0, (int)Lane.Third, 0, (int)Lane.Fourth, 0)]
        [TestCase((int)Lane.Second, 0, (int)Lane.Third, 0, (int)Lane.Fourth, 0)]
        [TestCase((int)Lane.First, 0, (int)Lane.Second, 0, (int)Lane.Fourth, 0)]
        [TestCase((int)Lane.First, 0, (int)Lane.Second, 0, (int)Lane.Third, 0)]
        [TestCase((int)Lane.First, 0, (int)Lane.Second, 0, (int)Lane.Fourth, 0)]
        [TestCase((int)Lane.Second, 0, (int)Lane.Third, 0, (int)Lane.First, 0)]
        [TestCase((int)Lane.Second, -100, (int)Lane.Third, -50, (int)Lane.First, 0)]
        public void WhichLanesAreFree_HowManyLanesAreFreeWithThreeCars(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList[0].Pos.X = x1;
            _randomCars.RandomCarsList[0].Pos.Y = y1;
            _randomCars.RandomCarsList[1].Pos.X = x2;
            _randomCars.RandomCarsList[1].Pos.Y = y2;
            _randomCars.RandomCarsList[2].Pos.X = x3;
            _randomCars.RandomCarsList[2].Pos.Y = y3;
            var freeLanes = StreetSingleton.GetInstance().WhichLanesAreFree(_randomCars.RandomCarsList);

            Assert.AreEqual(3, freeLanes.Count);
        }

        [TestCase((int)Lane.First, (int)Lane.Second, (int)Lane.Third, (int)Lane.Fourth, (int)Lane.Fifth)]
        [TestCase((int)Lane.First, (int)Lane.Second, (int)Lane.Third, (int)Lane.Fourth, (int)Lane.Sixth)]
        [TestCase((int)Lane.First, (int)Lane.Second, (int)Lane.Third, (int)Lane.Fifth, (int)Lane.Sixth)]
        [TestCase((int)Lane.First, (int)Lane.Second, (int)Lane.Fourth, (int)Lane.Fifth, (int)Lane.Sixth)]
        [TestCase((int)Lane.First, (int)Lane.Third, (int)Lane.Fourth, (int)Lane.Fifth, (int)Lane.Sixth)]
        [TestCase((int)Lane.Third, (int)Lane.Second, (int)Lane.Fourth, (int)Lane.Fifth, (int)Lane.Sixth)]
        public void WhichLanesAreFree_HowManyLanesAreFreeWithFiveCars_0Free(int x1, int x2,int x3, int x4, int x5)
        {
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList[0].Pos.X = x1;
            _randomCars.RandomCarsList[0].Pos.Y = 0;
            _randomCars.RandomCarsList[1].Pos.X = x2;
            _randomCars.RandomCarsList[1].Pos.Y = 0;
            _randomCars.RandomCarsList[2].Pos.X = x3;
            _randomCars.RandomCarsList[2].Pos.Y = 0;
            _randomCars.RandomCarsList[3].Pos.X = x4;
            _randomCars.RandomCarsList[3].Pos.Y = 0;
            _randomCars.RandomCarsList[4].Pos.X = x5;
            _randomCars.RandomCarsList[4].Pos.Y = 0;
            var freeLanes = StreetSingleton.GetInstance().WhichLanesAreFree(_randomCars.RandomCarsList);

            Assert.AreEqual(0, freeLanes.Count);
        }
        [TestCase((int)Lane.First, (int)Lane.Second, (int)Lane.Third, (int)Lane.Fourth, (int)Lane.Fifth)]
        [TestCase((int)Lane.First, (int)Lane.Second, (int)Lane.Third, (int)Lane.Fourth, (int)Lane.Sixth)]
        [TestCase((int)Lane.First, (int)Lane.Second, (int)Lane.Third, (int)Lane.Fifth, (int)Lane.Sixth)]
        [TestCase((int)Lane.First, (int)Lane.Second, (int)Lane.Fourth, (int)Lane.Fifth, (int)Lane.Sixth)]
        [TestCase((int)Lane.First, (int)Lane.Third, (int)Lane.Fourth, (int)Lane.Fifth, (int)Lane.Sixth)]
        [TestCase((int)Lane.Third, (int)Lane.Second, (int)Lane.Fourth, (int)Lane.Fifth, (int)Lane.Sixth)]
        public void WhichLanesAreFree_HowManyLanesAreFreeWithFiveCars_2Free(int x1, int x2, int x3, int x4, int x5)
        {
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList.Add(new Car());
            _randomCars.RandomCarsList[0].Pos.X = x1;
            _randomCars.RandomCarsList[0].Pos.Y = 350;
            _randomCars.RandomCarsList[1].Pos.X = x2;
            _randomCars.RandomCarsList[1].Pos.Y = 0;
            _randomCars.RandomCarsList[2].Pos.X = x3;
            _randomCars.RandomCarsList[2].Pos.Y = 0;
            _randomCars.RandomCarsList[3].Pos.X = x4;
            _randomCars.RandomCarsList[3].Pos.Y = 0;
            _randomCars.RandomCarsList[4].Pos.X = x5;
            _randomCars.RandomCarsList[4].Pos.Y = 0;
            var freeLanes = StreetSingleton.GetInstance().WhichLanesAreFree(_randomCars.RandomCarsList);

            Assert.AreEqual(2, freeLanes.Count);
        }
    }
}