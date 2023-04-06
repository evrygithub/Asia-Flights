using Kendo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Repositories
{
    public interface FlightsRepository
    {
        public List<Flights> GetFlights(string fromplace, string toplace, string days, int pageno, int rowsperpage,string dateofjourney);

        public int GetTotalRows(string fromplace, string toplace, string days,string dateofjourney);
    }
}
