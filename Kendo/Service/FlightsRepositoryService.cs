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
    public class FlightsRepositoryService : FlightsRepository
    {
        private readonly IConfiguration _configuration;
        public FlightsRepositoryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Flights> GetFlights(string fromplace, string toplace, string days, int pageno, int rowsperpage,string dateofjourney)
        {
            List<Flights> flightslist = new List<Flights>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("airways")))
                {
                    SqlCommand command = new SqlCommand("usp_GetFlights", connection);
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@from",fromplace);
                    command.Parameters.AddWithValue("@to", toplace);
                    command.Parameters.AddWithValue("@days", days);
                    command.Parameters.AddWithValue("@PageNumber", pageno);
                    command.Parameters.AddWithValue("@RowsOfPage", rowsperpage);
                    command.Parameters.AddWithValue("@doj", dateofjourney);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable flightstable = new DataTable();
                    da.Fill(flightstable);

                    return flightslist=flightstable.AsEnumerable().Select(flightdata => new Flights
                    {
                        flight_no= flightdata.Field<string>("flight_no"),
                        company_name=flightdata.Field<string>("company_name"),
                        number_of_seats = flightdata.Field<int>("number_of_seats"),
                        flight_from = flightdata.Field<string>("flight_from"),
                        flight_to = flightdata.Field<string>("flight_to"),
                        arraival_time = flightdata.Field<string>("arraival_time"),
                        departure_time = flightdata.Field<string>("departure_time"),
                        days= flightdata.Field<string>("days"),
                        fare=flightdata.Field<int>("fare")
                    }).ToList();
                    
                }
            }
            catch(Exception ex)
            {
                return flightslist;
            }
            
        }

        public int GetTotalRows(string fromplace, string toplace, string days,string dateofjourney)
        {
            int rows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("airways")))
                {
                    SqlCommand command = new SqlCommand("usp_GetFlights_TotalFlights", connection);
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@from", fromplace);
                    command.Parameters.AddWithValue("@to", toplace);
                    command.Parameters.AddWithValue("@days", days);
                    command.Parameters.AddWithValue("@doj", dateofjourney);
                    rows = Convert.ToInt32(command.ExecuteScalar());
                    return rows;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
