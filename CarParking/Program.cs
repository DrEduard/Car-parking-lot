using CarParking.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParking
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IParkingLotService, ParkingLotService>()
                .AddSingleton<IMenu, Menu>()
                .BuildServiceProvider();

            var parkingLotService = serviceProvider.GetService<IParkingLotService>();
            parkingLotService.Initialize();

            var menu = serviceProvider.GetService<IMenu>();
            menu.ShowMenu();
        }
    }
}
