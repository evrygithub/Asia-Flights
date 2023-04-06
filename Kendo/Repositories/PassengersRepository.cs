using Kendo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Repositories
{
    public interface PassengersRepository
    {
        public bool AddPassengers(CreateTicket tickets,DateTime dateofbooking);
        public List<TicketDetails>GetPassengerCurrentlyBookedTickets(string doj,string cardno,DateTime dateofbookings,string fno);

    }
}
