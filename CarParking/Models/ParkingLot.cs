using System.Collections.Generic;

namespace CarParking.Models
{
    public class ParkingLot
    {
        public List<Car> Cars { get; }

        public List<ParkingInfo> ParkingInfos { get; }

        public ParkingLot()
        {
            Cars = new List<Car>();
            ParkingInfos = new List<ParkingInfo>();
        }
    }
}
