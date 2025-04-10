using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using RazorHotelDB25InClass.Services;

namespace HotelDBTest
{
    [TestClass]
    public class HotelServiceTest
    {
        [TestMethod]
        public void TestCreateDeleteHotel()
        {
            //Arrange
            IHotelService hotelService = new HotelService();
            List<Hotel> hotels = hotelService.GetAllHotelAsync().Result;

            //Act
            int numOfHotelBefore = hotels.Count;
            Hotel newHotel = new Hotel(123, "UnitTestHotel", "UnitTestVej 22");
            bool ok = hotelService.CreateHotelAsync(newHotel).Result;
            hotels = hotelService.GetAllHotelAsync().Result;
            int numOfHotelAfter = hotels.Count;
            Hotel? h = hotelService.DeleteHotelAsync(newHotel.HotelNr).Result;

            //Assert
            Assert.AreEqual(numOfHotelBefore + 1, numOfHotelAfter);
            Assert.IsTrue(ok);
            Assert.AreEqual(h.HotelNr, newHotel.HotelNr);
        }

        [TestMethod]
        public void TestUpdateHotel()
        {
            //Arrange
            IHotelService hotelService = new HotelService();
            List<Hotel> hotels = hotelService.GetAllHotelAsync().Result;

            //Act
            int numOfHotelBefore = hotels.Count;
            
            Hotel newHotel = new Hotel(123, "UnitTestHotel", "UnitTestVej 22");
            bool ok = hotelService.CreateHotelAsync(newHotel).Result;
            
            Hotel updateHotel = new Hotel(123, "UpdateTestHotel", "UpdateTestVej 33");
            bool updOk = hotelService.UpdateHotelAsync(updateHotel, newHotel.HotelNr).Result;

            hotels = hotelService.GetAllHotelAsync().Result;
            int numOfHotelAfter = hotels.Count;
            
            Hotel? h = hotelService.DeleteHotelAsync(newHotel.HotelNr).Result;

            //Assert
            Assert.AreEqual(numOfHotelBefore + 1, numOfHotelAfter);
            Assert.IsTrue(ok);
            Assert.IsTrue(updOk);
            Assert.AreEqual(h.Navn, updateHotel.Navn);
        }
    }
}