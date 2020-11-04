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
    }
}
