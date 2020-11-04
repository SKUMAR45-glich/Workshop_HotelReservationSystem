using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationSystem
{
    public class HotelReservation
    {
        public Dictionary<string, HotelDetails> hotelDetails;

        int cheapestrate = 0;                                             //Cheapest Rate Hotel

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

            Console.Write("Enter Weekday Rate for Customers: ");
            hotelDetails.weekdayrate = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Weekend Rate for Customers: ");
            hotelDetails.weekenddayrate = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Rating for Customers: ");
            hotelDetails.rating = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Reward Weekday Rate for Loyal Customers: ");
            hotelDetails.reward_weekdayrate = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Reward Weekend Rate for Loyal Customers: ");
            hotelDetails.reward_weekenddayrate = Convert.ToInt32(Console.ReadLine());

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

            var cheapestBestRatedHotel = GetBestRatedHotel(startDate, endDate);
            cheapestrate = GetTotalCost(cheapestBestRatedHotel, startDate, endDate);

            DisplayBestRatedHotel(cheapestBestRatedHotel);
        }


        //To find Cheapest Available Hotel 
        public List<HotelDetails> GetCheapestHotel(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                Console.WriteLine("Please enter a valid range of dates");
                return null;
            }
            int bill = Int32.MaxValue;
            List<HotelDetails> cheapestHotel = new List<HotelDetails>();
            foreach (var hotels in hotelDetails)
            {
                int temp = bill;
                bill = Math.Min(bill, GetTotalCost(hotels.Value, startDate, endDate));
                if (temp != bill)
                {
                    cheapestHotel.Add(hotels.Value);
                }
            }

            cheapestrate = bill;
            return cheapestHotel;
        }

        //To Find Cheapest Avaliable Best Rated Hotel
        public List<HotelDetails> GetCheapestBestRatedHotel(DateTime startDate, DateTime endDate)
        {
            var cheapestHotel = GetCheapestHotel(startDate, endDate);
            List<HotelDetails> cheapestBestRatedHotel = new List<HotelDetails>();

            int maxRating = Int32.MinValue;

            foreach (var hotel in cheapestHotel)
            {
                maxRating = Math.Max(maxRating, hotel.rating);
            }

            foreach (var hotel in cheapestHotel)
            {
                if (hotel.rating == maxRating)
                {
                    cheapestBestRatedHotel.Add(hotel);
                }
            }
            return cheapestBestRatedHotel;
        }


        //To Find Best Rated Hotel Available
        public HotelDetails GetBestRatedHotel(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                Console.WriteLine("Please enter a valid range of dates");
                return null;
            }

            HotelDetails bestRatedHotel = new HotelDetails();
            int maxRating = Int32.MinValue;

            foreach (var hotel in hotelDetails)
            {
                maxRating = Math.Max(maxRating, hotel.Value.rating);
            }

            foreach (var hotel in hotelDetails)
            {
                if (hotel.Value.rating == maxRating)
                {
                    bestRatedHotel = hotel.Value;
                }
            }
            return bestRatedHotel;
        }


        //Display Best Rated Hotel
        public void DisplayBestRatedHotel(HotelDetails bestRatedHotel)
        {
            Console.WriteLine("Hotel :" + bestRatedHotel.hotelname + "Total Bill" + cheapestrate);
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


        // Display Cheapest Best Rated Hotel 
        public void DisplayCheapestBestRatedHotel(List<HotelDetails> cheapestBestRatedHotel)
        {

            foreach (HotelDetails hotelDetails in cheapestBestRatedHotel)
            {
                Console.WriteLine("Hotel :" + hotelDetails.hotelname + "Total Bill" + cheapestrate);
            }
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
