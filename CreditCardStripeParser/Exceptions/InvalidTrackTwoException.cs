using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardStripeParser.Exceptions
{
   public class InvalidTrackTwoException : Exception
    {
        public InvalidTrackTwoException() : base("Invalid Track Two data")
        {
        }

        public InvalidTrackTwoException(string message)
            : base(message)
        {
        }

        public InvalidTrackTwoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
