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
        /// <summary>
        /// Adds the new hotel to Database.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="rating">The rating.</param>
        /// <param name="weekdayRegular">The weekday regular price.</param>
        /// <param name="weekdayReward">The weekday reward price.</param>
        /// <param name="weekendRegular">The weekend regular price.</param>
        /// <param name="weekendReward">The weekend reward price.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all hotels from DB.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// returns list of dates from Start Date to End Date.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        List<DateTime> DatesInRange(DateTime start, DateTime end)
        {
            List<DateTime> dates=new List<DateTime>();
            for (DateTime date = start.Date; date <= end.Date;date = date.AddDays(1))
                dates.Add(date);
            return dates;
        }

        /// <summary>
        /// Gets the cheapest Hotel on a date.
        /// </summary>
        /// <param name="hotels">The hotels.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        int GetCheapestOnDate(List<Hotel> hotels,DateTime date)
        {
            int minPrice = int.MaxValue;
            Hotel cheapestHotel = null;
            foreach(Hotel hotel in hotels)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (minPrice > hotel.WeekendRegular)
                    {
                        minPrice = hotel.WeekendRegular;
                        cheapestHotel = hotel;
                    }
                }
                else
                {
                    if (minPrice > hotel.WeekdayRegular)
                    {
                        minPrice = hotel.WeekdayRegular;
                        cheapestHotel = hotel;
                    }
                }
            }
            Console.Write(date.ToShortDateString()+": "+cheapestHotel.Location + ", ");
            return minPrice;
        }

        /// <summary>
        /// Gets the cheapest Bookings possible within given date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public int GetCheapest(DateTime startDate, DateTime endDate)
        {
            List<Hotel> hotels = GetAllHotels();
            List<int> prices = new List<int>();

            int price;
            foreach (Hotel hotel in hotels)
            {
                price = 0;
                foreach (DateTime date in DatesInRange(startDate, endDate))
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                        price += hotel.WeekendRegular;
                    else
                        price += hotel.WeekdayRegular;
                }
                prices.Add(price);
            }

            int maxRating = 0;
            Hotel best = null;
            for (int i = 0; i < prices.Count; i++)
            {
                if (prices[i] == prices.Min() && maxRating < hotels[i].Rating)
                    best = hotels[i];
            }


            Console.WriteLine("Best Hotel is " + best.Location + " Ratings " + best.Rating + " Minimum Cost is " + prices.Min());
            return prices.Min();
        }

        /// <summary>
        /// Gets the highest rated Hotel.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public int GetHighestRated(DateTime startDate, DateTime endDate)
        {
            List<Hotel> hotels = GetAllHotels();
            Hotel best = new Hotel();
            best.Rating = 0;
            foreach(Hotel hotel in hotels)
            {
                if (hotel.Rating > best.Rating)
                    best = hotel;
            }

            int total = 0;
            foreach (DateTime date in DatesInRange(startDate, endDate))
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    total += best.WeekendRegular;
                else
                    total += best.WeekdayRegular;

            Console.WriteLine("Best Hotel is " + best.Location + " Ratings " + best.Rating + " Minimum Cost is " + total);
            return total;
        }


    }
}
