namespace Parking
{
    public class Tariff
    {
        public string TariffName { get; set; }
        public int Minutes { get; set; }
        public int Cost { get; set; }

        public Tariff(string tariffName, int minutes, int cost)
        {
            TariffName = tariffName;
            Minutes = minutes;
            Cost = cost;
        }
    }
}
