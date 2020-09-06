using CarParking.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarParking.Services
{
    public interface IParkingLotService
    {
        ParkingLot ParkingLot { get; }

        void Initialize();

        void AddCar(Car car, DateTime? timeEnter = null);

        void RemoveCar(string carNumber);

        int GetAvailableSpaces();

        List<ParkingInfo> GetCurrentParkingLotInfo();

        ParkingInfo GetParkingInfo(string carNumber);
    }

    public class ParkingLotService : IParkingLotService
    {
        private int AvailableSpaces = 10;

        private object _locker = new object();

        public ParkingLot ParkingLot { get; private set; }

        public void Initialize()
        {
            ParkingLot = new ParkingLot();
        }

        public void AddCar(Car car, DateTime? timeEnter = null)
        {
            lock (_locker)
            {
                if (car == null)
                {
                    throw new ArgumentNullException("Car cannot be null");
                }

                if (ParkingLot.Cars.Count == AvailableSpaces)
                {
                    throw new ParkingLotFullException();
                }

                if (ParkingLot.Cars.Any(x => x.Number.Equals(car.Number)))
                {
                    throw new NotSupportedException("A car with the same number is already inside");
                }

                var parkingInfo = new ParkingInfo(car.Number, timeEnter);
                ParkingLot.ParkingInfos.Add(parkingInfo);

                ParkingLot.Cars.Add(car);
            }
        }

        public void RemoveCar(string carNumber)
        {
            lock (_locker)
            {
                var car = ParkingLot.Cars.FirstOrDefault(x => x.Number.Equals(carNumber));

                if (car == null)
                {
                    throw new NotSupportedException("No cars with this number exist in the parking lot");
                }

                var parkingInfo = ParkingLot.ParkingInfos.First(x => x.CarNumber.Equals(carNumber));
                parkingInfo.LeaveHour = DateTime.Now;

                ParkingLot.Cars.Remove(car);
            }
        }

        public int GetAvailableSpaces()
        {
            return AvailableSpaces - ParkingLot.Cars.Count;
        }

        public List<ParkingInfo> GetCurrentParkingLotInfo()
        {
            return ParkingLot.ParkingInfos.Where(x => !x.LeaveHour.HasValue).ToList();
        }

        public ParkingInfo GetParkingInfo(string carNumber)
        {
            return ParkingLot.ParkingInfos.FirstOrDefault(x => x.CarNumber.Equals(carNumber));
        }
    }

    public class ParkingLotFullException : Exception
    {
        public ParkingLotFullException() : base("Parking lot is full")
        {
        }
    }

}
