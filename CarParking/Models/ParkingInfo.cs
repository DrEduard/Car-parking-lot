using System;
using System.Configuration;

namespace CarParking.Models
{
    public class ParkingInfo
    {
        private int FirstHourPrice = Convert.ToInt32(ConfigurationManager.AppSettings["FirstHourPrice"].ToString());

        private int OtherHoursPrice = Convert.ToInt32(ConfigurationManager.AppSettings["OtherHoursPrice"].ToString());

        public string CarNumber { get; }
        public DateTime EnterHour { get; }
        public DateTime? LeaveHour { get; set; }
        public double Cost => GetCost();
        public double Minutes => LeaveHour.HasValue ? LeaveHour.Value.Subtract(EnterHour).TotalMinutes : DateTime.Now.Subtract(EnterHour).TotalMinutes;

        public ParkingInfo(string carNumber, DateTime? timeEnter = null)
        {
            timeEnter = timeEnter ?? DateTime.Now;

            EnterHour = timeEnter.Value;
            CarNumber = carNumber;
        }

        private double GetCost()
        {
            var finalHour = LeaveHour ?? DateTime.Now;
            var minutes = finalHour.Subtract(EnterHour).TotalMinutes;
            var price = FirstHourPrice;

            if (minutes <= 60)
            {
                return price;
            }

            return price + Math.Ceiling((minutes - 60) / 60) * OtherHoursPrice;
        }
    }
}
