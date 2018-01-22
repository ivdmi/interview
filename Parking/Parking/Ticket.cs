using System;

namespace Parking
{
    class Ticket
    {
        public string CarNumber { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public DateTime TimePaid { get; set; }

        public Ticket(string carNumber)
        {
            CarNumber = carNumber;
            TimeIn = DateTime.Now;
        }
    }
}
