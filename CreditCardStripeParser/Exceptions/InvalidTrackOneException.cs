using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardStripeParser.Exceptions
{
   public class InvalidTrackOneException : Exception
    {
        public InvalidTrackOneException() : base("Invalid Track One data")
        {
        }

        public InvalidTrackOneException(string message)
            : base(message)
        {
        }

        public InvalidTrackOneException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
