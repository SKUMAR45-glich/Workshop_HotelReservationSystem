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
        }

        ExceptionType type;

        public CustomExceptions(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
