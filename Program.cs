using System;

namespace HotelReservationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to The Hotel Reservation System in Miami");

            HotelReservation hotelReservation = new HotelReservation();

            hotelReservation.GetDetailsfortheHotel();              //Add Name Week and Weekend Rates to the Hotel System


            hotelReservation.GettheDateRangeforBooking();           // To get the Best Rated Hotel Available

            hotelReservation.DisplayHotels();                       //To Display the Details of the Hotel
        }
    }
}
