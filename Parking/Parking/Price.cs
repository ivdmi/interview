using System.Collections.Generic;
using System.Linq;

namespace Parking
{
    public interface IPrice
    {
        Tariff GetTariff(string tariffName);
        Tariff GetTariff(int minutes);
        Tariff GetTariff(int hours, int minutes);
        Tariff GetDailyTariff();
        void AddTariff(Tariff tariff);
        void RemoveTariff(Tariff tariff);
    }

    public class Price : IPrice
    {
        private IList<Tariff> Tariffs { get; set; } = new List<Tariff>();

        public Tariff GetTariff(string tariffName)
        {
            var tariff = Tariffs.FirstOrDefault(t => t.TariffName.Equals(tariffName));
            return tariff;
        }

        // вернуть ближайший тариф по времени
        public Tariff GetTariff(int minutes)
        {
            // var tariff = Tariffs.OrderByDescending(t => t.Minutes).FirstOrDefault(t => t.Minutes <= minutes);

            // TODO: можно поработать над оптимальным выбором тарифа
            var tariff = Tariffs.OrderBy(t => t.Minutes).FirstOrDefault(t => t.Minutes >= minutes);
            
            return tariff;
        }

        public Tariff GetTariff(int hours, int minutes)
        {
            int totalMinutes = hours * 60 + minutes;

            return GetTariff(totalMinutes);
        }

        public Tariff GetDailyTariff()
        {
            int totalMinutes = 60 * 24;

            return GetTariff(totalMinutes);
        }

        public void AddTariff(Tariff tariff)
        {
            Tariffs.Add(tariff);
        }

        public void RemoveTariff(Tariff tariff)
        {
            Tariffs.Remove(tariff);
        }

    }
}
