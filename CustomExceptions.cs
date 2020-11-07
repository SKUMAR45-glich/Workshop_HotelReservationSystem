using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservationSystem
{
    public class CustomExceptions : Exception
    {
        public enum ExceptionType
        {
            INVALID_NAME,
            NO_VALUE_ENTRIES,
            INVALID_RATING,
            INVALID_RATE,
            INVALID_RANGE_OF_DATE,
            INVALID_CUST_TYPE,
        }

        ExceptionType type;

        public CustomExceptions(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
