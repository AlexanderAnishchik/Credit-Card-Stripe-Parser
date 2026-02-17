using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardStripeParser.Exceptions
{
    /// <summary>
    /// Exception thrown when Track 2 data is invalid or cannot be parsed.
    /// </summary>
   public class InvalidTrackTwoException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTrackTwoException"/> class with a default message.
        /// </summary>
        public InvalidTrackTwoException() : base("Invalid Track Two data")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTrackTwoException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidTrackTwoException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTrackTwoException"/> class with a specified error message and a reference to the inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public InvalidTrackTwoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
