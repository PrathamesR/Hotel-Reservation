using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation
{
    public class Hotel
    {
        public string Location { get; set; }
        public int Rating { get; set; }
        public int WeekdayRegular { get; set; }
        public int WeekdayReward { get; set; }
        public int WeekendRegular { get; set; }
        public int WeekendReward { get; set; }
    }
}
