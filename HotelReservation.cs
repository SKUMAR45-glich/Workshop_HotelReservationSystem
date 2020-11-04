using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationSystem
{
    public class HotelReservation
    {
        public Dictionary<string, HotelDetails> hotelDetails;


        //Constructor
        public HotelReservation()
        {
            hotelDetails = new Dictionary<string, HotelDetails>();
        }

        //To Add Hotel details
        public void AddHotel(HotelDetails hotelDetails)
        {
            if (this.hotelDetails.ContainsKey(hotelDetails.hotelname))
            {
                Console.WriteLine("This Hotel is Already Present in Miami");
                return;
            }
            this.hotelDetails.Add(hotelDetails.hotelname, hotelDetails);
        }

        //Enter Details from the User

        public void GetDetailsfortheHotel()
        {
            HotelDetails hotelDetails = new HotelDetails();
            Console.Write("Enter Name of the Hotel : ");
            hotelDetails.hotelname = Console.ReadLine();

            Console.Write("Enter Weekday Rate for Customer: ");
            hotelDetails.weekdayrate = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Weekend Rate for Customer: ");
            hotelDetails.weekenddayrate = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Rating for Customer: ");
            hotelDetails.rating = Convert.ToInt32(Console.ReadLine());

            AddHotel(hotelDetails);
        }

        //Get the Range of Date

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


        //To find Cheapest Available Hotel 
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


        //Calculate the total bill
        public int GetTotalCost(HotelDetails hotelDetails, DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate.Subtract(startDate);
            int dateRange = Convert.ToInt32(timeSpan.TotalDays);
            int weekDays = CheckforWeekDays(startDate, endDate);
            int weekEnds = dateRange - weekDays;

            int totalRate = (weekDays * hotelDetails.weekdayrate) + (weekEnds * hotelDetails.weekenddayrate);
            return totalRate;
        }

        //Get the Number of Week days 
        public int CheckforWeekDays(DateTime startDate, DateTime endDate)
        {
            int numberofDays = 0;
            while (startDate <= endDate)
            {
                ////Checking start days is Weekend or not
                if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    numberofDays++;
                }

                startDate = startDate.AddDays(1);
            }
            return numberofDays;
        }



        //Display Details
        public string DisplayHotels()
        {
            Console.WriteLine("Welcome to the Hotels\n");
            try
            {
                foreach (var hotel in this.hotelDetails.Values)
                {
                    return $"Hotel Name: {hotel.hotelname}, RegularRate: {hotel.weekdayrate}, WeekendRate: {hotel.weekenddayrate} and Rating: {hotel.rating}";
                }
            }
            catch (CustomExceptions)
            {
                throw new CustomExceptions(CustomExceptions.ExceptionType.INVALID_NAME, "Please Enter valid Name of Hotel");

            }
            return "Wrong Name";
        }

    }
}
