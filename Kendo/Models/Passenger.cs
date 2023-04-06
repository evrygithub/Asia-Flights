using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Models
{
    public class Passenger
    {
        public string dateofjourney { get; set; }
        public string flightno { get; set; }

        public string passengername { get; set; }
        public int age { get; set; }
        public string gender { get; set; }

        public string flightfrom { get; set; }

        public string flightto { get; set; }

        public string paymentmethod { get; set; }

        public string carddetails { get; set; }

        public int fare { get; set; }
    }
}
