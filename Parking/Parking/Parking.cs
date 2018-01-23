using System;
using System.Collections.Generic;
using System.Linq;

namespace Parking
{
	// подготовка к собеседованию
    class Parking
    {
        public Parking(int placeTotal, IPrice price)
        {
            PlacesTotal = placeTotal;
            Price = price;
            CarList = new List<Ticket>();
        }

        public Random rnd = new Random();
        public IPrice Price { get; set; }
        public int PlacesTotal { get; set; }
        public int PlacesFree { get; set; }
        public IList<Ticket> CarList { get; set; }
        

        public void CarDroveInParking(string carNumber)
        {
            Console.WriteLine("Автомобиль {0} заехал в паркинг {1}", carNumber, DateTime.Now);
            CarList.Add(new Ticket(carNumber));
        }

        public void CarLeftParking(string carNumber)
        {
            var car = CarList.FirstOrDefault(c => c.CarNumber.Equals(carNumber));
            if (car != null)
            {
                int min = rnd.Next(500);
                car.TimeOut = DateTime.Now.AddMinutes(min);      // эмуляция простоя
                if (IsPayed(car))
                {
                    Console.WriteLine("{0} выехал из паркинга в {1}", car.CarNumber, car.TimeOut);
                    CarList.Remove(car);
                }
                else
                {
                    var minutes = (int)(car.TimeOut - car.TimeIn).TotalMinutes;
                    var cost = Price.GetTariff(minutes).Cost;
                    Console.WriteLine("{0} не оплатил {1}USD паркинг с {2} по {3}", car.CarNumber, cost, car.TimeIn, car.TimeOut);
                }
            }
        }

        public bool IsPayed(Ticket ticket)
        {
            if (ticket.TimePaid >= ticket.TimeOut)
                return true;
            else
            {
                return false;
            }
        }

        public bool Pay(string carNumber, int minutes, int cost)
        {
            bool paySacess = false;

            var car = CarList.FirstOrDefault(c => c.CarNumber.Equals(carNumber));

            var tariff = Price.GetTariff(minutes);

            if (cost < tariff.Cost)
                Console.WriteLine("{0} денег для оплаты не достаточно");
            else
            {
                car.TimePaid = car.TimeIn.AddMinutes(tariff.Minutes);
                paySacess = true;
            }
            
            return paySacess;
        }

        public void GetCars()
        {
            Console.WriteLine("Список машин в паркинге");
            foreach (var car in CarList)
            {
                Console.WriteLine("{0} \t {1}", car.CarNumber, car.TimeIn);
            }
        }
    }
}
