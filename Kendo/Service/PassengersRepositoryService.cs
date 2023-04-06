using Kendo.Models;
using Kendo.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Service
{
    public class PassengersRepositoryService : PassengersRepository
    {
        private readonly IConfiguration _configuration;
        public PassengersRepositoryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool AddPassengers(CreateTicket tickets,DateTime dateofbooking)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("airways")))
                {
                    foreach (var item in tickets.passengers)
                    {
                        SqlCommand command = new SqlCommand("usp_AddPassengers", con);
                        con.Open();
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@doj", item.dateofjourney);
                        command.Parameters.AddWithValue("@fno", item.flightno);
                        command.Parameters.AddWithValue("@passengername", item.passengername);
                        command.Parameters.AddWithValue("@age", item.age);
                        command.Parameters.AddWithValue("@gender", item.gender);
                        command.Parameters.AddWithValue("@from", item.flightfrom);
                        command.Parameters.AddWithValue("@to", item.flightto);
                        command.Parameters.AddWithValue("@paymentmethod", item.paymentmethod);
                        command.Parameters.AddWithValue("@cardnumber", item.carddetails);
                        command.Parameters.AddWithValue("@fare", item.fare);
                        command.Parameters.AddWithValue("@dateofbooking", dateofbooking);
                        i = Convert.ToInt32(command.ExecuteScalar());
                        con.Close();
                    }
                    if (i == 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TicketDetails> GetPassengerCurrentlyBookedTickets(string doj,string cardno,DateTime dateofbookings,string fno)
        {
            List<TicketDetails> lst = new List<TicketDetails>();
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("airways")))
                {
                    SqlCommand command = new SqlCommand("usp_GetTicket", con);
                    con.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    command.Parameters.AddWithValue("@doj", doj);
                    command.Parameters.AddWithValue("@cardno", cardno);
                    command.Parameters.AddWithValue("@dateofbooking", dateofbookings);
                    command.Parameters.AddWithValue("@fno", fno);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return lst = dt.AsEnumerable().Select(x => new TicketDetails
                    {
                        seatno = x.Field<int>("seatno"),
                        ticketno = x.Field<int>("ticketno"),
                        dateofjourney = x.Field<string>("date_of_journey"),
                        flightno = x.Field<string>("flight_no"),
                        passengername = x.Field<string>("passenger_name"),
                        age = x.Field<int>("age"),
                        gender = x.Field<string>("gender"),
                        flightfrom = x.Field<string>("flight_from"),
                        flightto = x.Field<string>("flight_to"),
                        fare = x.Field<int>("fare"),
                        dateofbooking=x.Field<string>("Dateofbooking"),
                        arrivaltime=x.Field<string>("arraivaltime"),
                        departuretime=x.Field<string>("departuretime")
                    }).ToList();
                }
            }
            catch(Exception ex)
            {
                return lst;
            }
            
        }

        
    }
}
