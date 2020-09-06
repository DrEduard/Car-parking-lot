using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParking.Models
{
    public class Car
    {
        public string Number { get; }

        public Car(string number)
        {
            Number = number;
        }
    }
}
