using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HotelReservation
{
    public class HotelManager
    {
        public bool AddNewHotel(string location,int rating,int weekdayRegular, int weekdayReward,int weekendRegular, int weekendReward)
        {
            SqlConnection connection = new SqlConnection(@"Data Source='(LocalDB)\MSSQL Server';Initial Catalog=HotelReservation;Integrated Security=True");
            
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddNewHotel", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Location",location);
                command.Parameters.AddWithValue("@Rating",rating);
                command.Parameters.AddWithValue("@WeekdayRegular",weekdayRegular);
                command.Parameters.AddWithValue("@WeekdayReward",weekdayReward);
                command.Parameters.AddWithValue("@WeekendRegular",weekendRegular);
                command.Parameters.AddWithValue("@WeekendReward",weekendReward);

                SqlDataReader dataReader = command.ExecuteReader();
                return true;

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        List<Hotel> GetAllHotels()
        {
            string conn = @"Data Source='(LocalDB)\MSSQL Server';Initial Catalog=HotelReservation;Integrated Security=True";
            SqlConnection connection = new SqlConnection(conn);

            try
            {
                connection.Open();
                string query = "select * from Hotels";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader dataReader = command.ExecuteReader();

                List<Hotel> hotels = new List<Hotel>();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Hotel hotel = new Hotel();
                        hotel.Location = dataReader.GetString(0);
                        hotel.Rating = dataReader.GetInt32(1);
                        hotel.WeekdayRegular = dataReader.GetInt32(2);
                        hotel.WeekdayReward = dataReader.GetInt32(3);
                        hotel.WeekendRegular = dataReader.GetInt32(4);
                        hotel.WeekendReward = dataReader.GetInt32(5);

                        hotels.Add(hotel);
                    }
                }
                return hotels;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        List<DateTime> Dates(DateTime start, DateTime end)
        {
            List<DateTime> dates=new List<DateTime>();
            for (DateTime date = start.Date; date <= end.Date;date = date.AddDays(1))
                dates.Add(date);
            return dates;
        }

        public int GetCheapest(DateTime startDate, DateTime endDate)
        {
            List<Hotel> hotels = GetAllHotels();
            List<int> prices = new List<int>();

            foreach(Hotel hotel in hotels)
            {
                int price=0;
                foreach (DateTime date in Dates(startDate, endDate))
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                        price += hotel.WeekendRegular;
                    else
                        price += hotel.WeekdayRegular;
                }
                prices.Add(price);
            }

            int cheapest = prices.IndexOf(prices.Min());
            Console.WriteLine("Cheapest Hotel : " + hotels[cheapest].Location + " Price :$" + prices[cheapest]);
            return prices[cheapest];
        }


    }
}
