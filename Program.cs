using System;

namespace HotelReservationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to The Hotel Reservation System in Miami");

            HotelReservation hotelReservation = new HotelReservation();

            hotelReservation.GetDetailsfortheHotel();              //Add Name, Week Rates, Weekend Rates, Rating and Special Rewards to the Hotel System


            hotelReservation.GettheDateRangeforBooking();           // To get the Cheapest Best Rated Hotel Available for Reward Customers

            hotelReservation.DisplayHotels();                       //To Display the Details of the Hotel
        }
    }
}
