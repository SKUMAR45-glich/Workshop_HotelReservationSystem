using HotelReservationSystem;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace HotelReservationSystemTest
{
    public class Tests
    {
        HotelReservation hotelReservation = new HotelReservation();


        //Initializing the Hotel Data
        [SetUp]
        public void Setup()
        {
            hotelReservation.AddHotel(new HotelDetails { hotelname = "Lakewood", weekdayrate = 110, weekenddayrate = 90, rating = 3 });
            hotelReservation.AddHotel(new HotelDetails{ hotelname = "Bridgewood", weekdayrate = 160, weekenddayrate = 60, rating = 4 });
            hotelReservation.AddHotel(new HotelDetails { hotelname = "Ridgewood", weekdayrate = 220, weekenddayrate = 150, rating = 5 });
        }


        //Get the Cheapest Hotel in the Date Range
        [Test]
        public void GetCheapestHotelinaDateRange()
        {
            string start = "11 Sept 2020";
            DateTime startDate = DateTime.Parse(start);
            string end = "12 Sept 2020";
            DateTime endDate = DateTime.Parse(end);

            var expected = hotelReservation.hotelDetails["Lakewood"];
            var result = hotelReservation.GetCheapestHotel(startDate, endDate);

            Assert.Equals(result, expected);
        }


        //Get the Cheapest Best Rated Hotel in the Date Range

        [Test]
        public void GetCheapestBestRatedHotelinaDateRange()
        {
            string start = "11 Sept 2020";
            DateTime startDate = DateTime.Parse(start);
            string end = "12 Sept 2020";
            DateTime endDate = DateTime.Parse(end);

            var expected = hotelReservation.hotelDetails["Bridgewood"];
            var result = hotelReservation.GetCheapestBestRatedHotel(startDate, endDate);

            Assert.Equals(result, expected);
        }

        //Get the Best Rated Hotel in the Date Range

        [Test]
        public void GettheBestRatedHotelinaDateRange()
        {
            string start = "11 Sept 2020";
            DateTime startDate = DateTime.Parse(start);
            string end = "12 Sept 2020";
            DateTime endDate = DateTime.Parse(end);

            var expected = hotelReservation.hotelDetails["Ridgewood"];
            var result = hotelReservation.GetBestRatedHotel(startDate, endDate);

            Assert.Equals(result, expected);
        }
    }
}