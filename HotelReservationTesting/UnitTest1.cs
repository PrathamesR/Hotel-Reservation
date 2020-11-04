using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelReservation;

namespace HotelReservationTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            HotelManager hotelManager = new HotelManager();
            bool success = hotelManager.AddNewHotel("Ridgewood", 5, 220, 100, 150, 40);

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestMethod2()
        {
            HotelManager hotelManager = new HotelManager();
            int price = hotelManager.GetCheapest(new System.DateTime(2020,9,10),new System.DateTime(2020,9,11));

            Assert.AreEqual(220, price);
        }
    }
}
