using Kendo.Models;
using Kendo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
namespace Kendo.Controllers
{
    public class FlightsController : Controller
    {
        private FlightsRepository _employeerepository;
        private PassengersRepository _passengersRepository;
        public static List<Passenger> passengerlist = new List<Passenger>();
        public static DateTime dateofbookings;
        public FlightsController(FlightsRepository employeerepository, PassengersRepository passengersRepository)
        {
            _employeerepository =employeerepository;
            _passengersRepository = passengersRepository;
        }
        [HttpGet]
        public IActionResult GetFlights()
        {
           
            return View();
        }
        [HttpGet]
        public IActionResult GetTotalRows(string fromplace, string toplace, string days,string dateofjourney)
        {
            int totalrows= _employeerepository.GetTotalRows(fromplace, toplace, days,dateofjourney);

            return Json(totalrows);
        }
        [HttpGet]
        public IActionResult GetAllFlights(string fromplace, string toplace, string days, int pageno, int rowsperpage,string dateofjourney)
        {
            List<Flights> lstflights = _employeerepository.GetFlights(fromplace,toplace,days,pageno, rowsperpage,dateofjourney);
            
            return Json(lstflights);
        }

        [HttpGet]
        public IActionResult ReserveTicket(string fno,string from, string to, int fare, string dateofjourney)
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult AddPassenger(CreateTicket createTicket)
        {
            dateofbookings = DateTime.Now;

            passengerlist = createTicket.passengers;
            bool status = _passengersRepository.AddPassengers(createTicket,dateofbookings);
            if (status == true)
                return Json(status);
            else
                return Json(false);
            
        }

        [HttpGet]
        public IActionResult Thanks()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ThankYou()
        {
            string doj = string.Empty; string cardno = string.Empty;
            string fno = string.Empty;
            foreach (var item in passengerlist.Take(1))// Take 1st item
            {
                
                doj = item.dateofjourney;
                cardno = item.carddetails;
                fno = item.flightno;
            }
            List<TicketDetails>lstticket=_passengersRepository.GetPassengerCurrentlyBookedTickets(doj,cardno,dateofbookings,fno);
            return Json(lstticket);
        }
        
        [HttpGet]

        public IActionResult Test()
        {

            return View();
        }
        

    }
}
