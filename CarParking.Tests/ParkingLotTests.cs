using CarParking.Models;
using CarParking.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CarParking.Tests
{
    [TestClass]
    public class ParkingLotTests
    {
        [TestMethod]
        public void AddCarSameNumber_ShouldThrowException()
        {
            var parkingLotService = GetParkingLotService();
            parkingLotService.AddCar(new Models.Car("12345"));

            Assert.ThrowsException<NotSupportedException>(() => parkingLotService.AddCar(new Models.Car("12345")));
        }

        [TestMethod]
        public void AddCar_WhenParkingLotFull_ShouldThrowException()
        {
            var parkingLotService = GetParkingLotService();

            parkingLotService.AddCar(new Car("1234510"));
            parkingLotService.AddCar(new Car("123459"));
            parkingLotService.AddCar(new Car("123458"));
            parkingLotService.AddCar(new Car("123457"));
            parkingLotService.AddCar(new Car("123456"));
            parkingLotService.AddCar(new Car("123455"));
            parkingLotService.AddCar(new Car("123454"));
            parkingLotService.AddCar(new Car("123453"));
            parkingLotService.AddCar(new Car("123452"));
            parkingLotService.AddCar(new Car("123451"));

            Assert.ThrowsException<ParkingLotFullException>(() => parkingLotService.AddCar(new Car("11")));
        }

        [TestMethod]
        public void AddNullCar_ShouldThrowException()
        {
            var parkingLotService = GetParkingLotService();

            Assert.ThrowsException<ArgumentNullException>(() => parkingLotService.AddCar(null));
        }

        [TestMethod]
        public void PriceShouldMatch()
        {
            var parkingLotService = GetParkingLotService();

            var car = new Car("1234");
            parkingLotService.AddCar(car, DateTime.Now.Subtract(TimeSpan.FromMinutes(181)));

            var parkingInfo = parkingLotService.GetParkingInfo("1234");

            Assert.AreEqual(25, parkingInfo.Cost, "Price is not calculated correctly");
        }

        [TestMethod]
        public void RemoveSameCarSecondTime_ShouldThrowException()
        {
            var parkingLotService = GetParkingLotService();

            parkingLotService.AddCar(new Car("1111"));
            parkingLotService.RemoveCar("1111");

            Assert.ThrowsException<NotSupportedException>(() => parkingLotService.RemoveCar("1111"));
        }

        private IParkingLotService GetParkingLotService()
        {
            var parkingLotService = new ParkingLotService();
            parkingLotService.Initialize();

            return parkingLotService;
        }
    }
}
