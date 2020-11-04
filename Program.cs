using System;

namespace HotelReservationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to The Hotel Reservation System in Miami");

            HotelReservation hotelReservation = new HotelReservation();
            hotelReservation.GetDetailsfortheHotel();
        }
    }
}
