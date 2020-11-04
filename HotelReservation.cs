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

        public void GettheDateRangeforBooking()
        {
            Console.WriteLine("Enter the date range for booking of Hotel");
            string range = Console.ReadLine();

            string[] date = range.Split(',');

            var startDate = Convert.ToDateTime(date[0]);
            var endDate = Convert.ToDateTime(date[1]);

            var cheapestHotel = GetCheapestHotel(startDate, endDate);
            var bill = GetTotalCost(cheapestHotel, startDate, endDate);

            Console.WriteLine($"{cheapestHotel.hotelname}, Total Rates {bill}");
        }

        public HotelDetails GetCheapestHotel(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                Console.WriteLine("Please enter a valid range of dates");
                return null;
            }
            int bill = Int32.MaxValue;
            HotelDetails cheapestHotel = new HotelDetails();
            foreach (var hotels in hotelDetails)
            {
                int temp = bill;
                bill = Math.Min(bill, GetTotalCost(hotels.Value, startDate, endDate));
                if(temp!=bill)
                {
                    cheapestHotel = hotels.Value;
                }
            }
            return cheapestHotel;
        }

        public int GetTotalCost(HotelDetails hotelDetails, DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate.Subtract(startDate);
            int dateRange = Convert.ToInt32(timeSpan.TotalDays);
            
            int totalRate = (hotelDetails.weekdayrate * dateRange);
            return totalRate;
        }

    }
}
