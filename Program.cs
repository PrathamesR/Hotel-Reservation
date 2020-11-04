using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hotel Reservation Problem");


            HotelManager hotelManager = new HotelManager();
            int prices = hotelManager.GetCheapest(new System.DateTime(2020, 9, 10), new System.DateTime(2020, 9, 11));

            Console.Read();
        }
    }
}
