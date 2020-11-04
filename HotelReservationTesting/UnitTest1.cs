using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelReservation;

namespace HotelReservationTesting
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Tests if data is getting added successfully.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            HotelManager hotelManager = new HotelManager();
            bool success = hotelManager.AddNewHotel("Ridgewood", 5, 220, 100, 150, 40);

            Assert.IsTrue(success);
        }

        /// <summary>
        /// Tests if cheapest hotel is getting calculated on weekdays.
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            HotelManager hotelManager = new HotelManager();
            int price = hotelManager.GetCheapest(new System.DateTime(2020, 9, 10), new System.DateTime(2020, 9, 11));

            Assert.AreEqual(220, price);
        }

        /// <summary>
        /// Tests if cheapest hotel is getting calculated on combination of weekdays and weekends.
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            HotelManager hotelManager = new HotelManager();
            int price = hotelManager.GetCheapest(new System.DateTime(2020, 9, 11), new System.DateTime(2020, 9, 12));

            Assert.AreEqual(200, price);
        }

        /// <summary>
        /// Tests if cheapest highest rated hotel is getting calculated on combination of weekdays and weekends.
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            HotelManager hotelManager = new HotelManager();
            int price = hotelManager.GetCheapest(new System.DateTime(2020, 9, 11), new System.DateTime(2020, 9, 12));

            Assert.AreEqual(200, price);
        }
    }
}
