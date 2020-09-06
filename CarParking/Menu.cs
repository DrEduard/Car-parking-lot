using CarParking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParking
{
    public interface IMenu
    {
        void ShowMenu(bool showMainMenu = true);
    }

    public class Menu: IMenu
    {
        private IParkingLotService _parkingLotService;

        public Menu(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        public void ShowMenu(bool showMainMenu = true)
        {
            if (showMainMenu)
            {
                ShowMainMenu();
            }

            var key = Console.ReadKey();
            Console.WriteLine("");

            switch (key.KeyChar)
            {
                case '1':
                    ShowAvaliableSpaces();
                    break;
                case '2':
                    ShowCurrentParkingLotInfo();
                    break;
                case '3':
                    ShowAddCar();
                    break;
                case '4':
                    ShowRemoveCar();
                    break;
                default:
                    ShowMenu();
                    break;
            }
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("1 - Show available spaces");
            Console.WriteLine("2 - Show cars in parking lot");
            Console.WriteLine("3 - Add car");
            Console.WriteLine("4 - Remove car");
            Console.WriteLine("");
        }

        private void ShowAvaliableSpaces()
        {
            Console.WriteLine("");
            Console.WriteLine($"Available spaces: {_parkingLotService.GetAvailableSpaces()}");
            ShowMenu();
        }

        private void ShowCurrentParkingLotInfo()
        {
            Console.WriteLine("");
            var parkingInfos = _parkingLotService.GetCurrentParkingLotInfo();
            foreach (var info in parkingInfos)
            {
                Console.WriteLine($"Car number: {info.CarNumber}; Enter date: {info.EnterHour.ToString()}");
            }

            ShowMenu();
        }

        private void ShowAddCar()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter car number");

            try
            {
                var number = Console.ReadLine();
                _parkingLotService.AddCar(new Models.Car(number));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                ShowMenu();
            }
        }

        private void ShowRemoveCar()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter car number");

            try
            {
                var number = Console.ReadLine();
                _parkingLotService.RemoveCar(number);

                var parkingInfo = _parkingLotService.GetParkingInfo(number);
                Console.WriteLine($"Car number: {parkingInfo.CarNumber}; Time parked: {Convert.ToInt32(parkingInfo.Minutes)} minutes; Cost: {parkingInfo.Cost} lei");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                ShowMenu();
            }
        }
    }
}
