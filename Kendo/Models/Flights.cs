using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Models
{
    public class Flights
    {
        public string flight_no { get; set; }
        public string company_name { get; set; }
        public int number_of_seats { get; set; }
        public string flight_from { get; set; }
        public string flight_to { get; set; }
        public string arraival_time { get; set; }
        public string departure_time { get; set; }
        public string days { get; set; }
        public int fare { get; set; }
    }
}
