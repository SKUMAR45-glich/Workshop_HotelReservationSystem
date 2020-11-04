using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationSystem
{
    public class HotelReservation
    {
        public Dictionary<string, HotelDetails> hotelDetails;

        public HotelReservation()
        {
            hotelDetails = new Dictionary<string, HotelDetails>();
        }

        public void AddHotel(HotelDetails hotelDetails)
        {
            if (this.hotelDetails.ContainsKey(hotelDetails.hotelname))
            {
                Console.WriteLine("This Hotel is Already Present in Miami");
                return;
            }
            this.hotelDetails.Add(hotelDetails.hotelname, hotelDetails);
        }

        public void GetDetailsfortheHotel()
        {
            HotelDetails hotelDetails = new HotelDetails();
            Console.Write("Enter Name of the Hotel : ");
            hotelDetails.hotelname = Console.ReadLine();

            Console.Write("Enter Weekday Rate for Customer: ");
            hotelDetails.weekdayrate = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Weekend Rate for Customer: ");
            hotelDetails.weekenddayrate = Convert.ToInt32(Console.ReadLine());

            AddHotel(hotelDetails);
        }
    }
}
