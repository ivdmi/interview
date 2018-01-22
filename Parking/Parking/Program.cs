using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            const int ParkingPlace = 120;

            Price price = new Price();
            price.AddTariff(new Tariff("1-hour", 60, 5));
            price.AddTariff(new Tariff("3-hour", 180, 12));
            price.AddTariff(new Tariff("8-hour", 480, 25));
            price.AddTariff(new Tariff("24-hour", 1440, 35));

            Parking parking = new Parking(ParkingPlace, price);
            
        }
    }
}
