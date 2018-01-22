using System;
using System.Collections.Generic;
using System.Linq;

namespace Parking
{

    class Parking
    {
        public Parking(int placeTotal, Price price)
        {
            PlacesTotal = placeTotal;
            Price = price;
            CarList = new List<Ticket>();

        }

        public Price Price { get; set; }
        public int PlacesTotal { get; set; }
        public int PlacesFree { get; set; }
        public IList<Ticket> CarList { get; set; }
        

        public void AddCarToParking(string carNumber)
        {
            CarList.Add(new Ticket(carNumber));
        }

        public void RemoveCarFromParking(string carNumber)
        {
            var car = CarList.FirstOrDefault(c => c.CarNumber.Equals(carNumber));
            if (car != null)
            {
                CarList.Remove(car);
            }
        }

        public bool IsPayed(string carNumber)
        {
            return true;    // 
        }

        public bool Pay(string carNumber, int minutes, int cost)
        {
            bool paySacess = false;

            var car = CarList.FirstOrDefault(c => c.CarNumber.Equals(carNumber));

            var tariff = Price.GetTariff(minutes);

            if (cost < tariff.Cost)
                Console.WriteLine("Денег не достаточно");
            else
            {
                car.TimePaid = car.TimeIn.AddMinutes(tariff.Minutes);
                paySacess = true;
            }

            // var period = (car.TimeIn - DateTime.Now).Minutes;
            return paySacess;
        }

        


    }
}
