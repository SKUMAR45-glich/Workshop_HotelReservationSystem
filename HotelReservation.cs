using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HotelReservationSystem
{

    
    public class HotelReservation
    {
        public Dictionary<string, HotelDetails> hotelDetails;

        int cheapestrate = 0;                                                                 //Cheapest Rate Hotel
        string customerType = "";

        //Constructor
        public HotelReservation()
        {
            hotelDetails = new Dictionary<string, HotelDetails>();
        }


        //To Add Hotel details
        public void AddHotel(HotelDetails hotelDetails)
        {
            if (isValid(hotelDetails))
            {
                this.hotelDetails.Add(hotelDetails.hotelname, hotelDetails);
            }
            
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


            Console.Write("Enter the Customer Type: ");
            customerType = Console.ReadLine();

            if(ValidCustomerType(customerType))
            {
                AddHotel(hotelDetails);
            }
            
            else
            {
                throw new CustomExceptions(CustomExceptions.ExceptionType.INVALID_CUST_TYPE, "Please Enter Valid Customer Type");
            }

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
                throw new CustomExceptions(CustomExceptions.ExceptionType.INVALID_RANGE_OF_DATE, "Please Enter a valid Range of Date");
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
            int totalRate;
            if(customerType == "Regular")
            {
                totalRate = (weekDays * hotelDetails.weekdayrate) + (weekEnds * hotelDetails.weekenddayrate);
            }
            else
            {
                totalRate = (weekDays * hotelDetails.reward_weekdayrate) + (weekEnds * hotelDetails.reward_weekenddayrate);
            }
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


        //Check Validity of Hotel Entered
        public bool isValid(HotelDetails hotelDetails)
        {
            if (hotelDetails.hotelname == null)
            {
                throw new CustomExceptions(CustomExceptions.ExceptionType.NO_VALUE_ENTRIES, "Please enter some value, NULL values not allowed");
            }

            else if (hotelDetails.rating > 5 || hotelDetails.rating < 3)
            {
                throw new CustomExceptions(CustomExceptions.ExceptionType.INVALID_RATING, "Raing must be greater than 2 and less than or equal to 5 ");
            }
            else if (hotelDetails.weekdayrate == 0 || hotelDetails.weekenddayrate == 0 || hotelDetails.reward_weekdayrate == 0 || hotelDetails.reward_weekenddayrate == 0)
            {
                throw new CustomExceptions(CustomExceptions.ExceptionType.INVALID_RATE, "Please enter a valid Rate for customers (No free service allowed)");
            }
            else if (this.hotelDetails.ContainsKey(hotelDetails.hotelname))
            {
                throw new CustomExceptions(CustomExceptions.ExceptionType.INVALID_NAME, "Hotel Name Already Exists");
            }
            else
            {
                return true;
            }
        }

        //Check the validity of CustomerType

        public static bool ValidCustomerType(string custType)
        {
            string Cust_Type = "^(Reward|Regular)$";

            if(Regex.IsMatch(custType,Cust_Type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public enum CustomerType
        {
            Regular, Reward
        };

    }
}
