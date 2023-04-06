using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Models
{
    public class TicketDetails:Passenger
    {
        public int seatno { get; set; }
        public int ticketno { get; set; }

        public string dateofbooking { get; set; }
        public string arrivaltime { get; set; }
        public string departuretime { get; set; }

    }
}
