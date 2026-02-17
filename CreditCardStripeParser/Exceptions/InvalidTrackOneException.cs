using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardStripeParser.Exceptions
{
    /// <summary>
    /// Exception thrown when Track 1 data is invalid or cannot be parsed.
    /// </summary>
   public class InvalidTrackOneException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTrackOneException"/> class with a default message.
        /// </summary>
        public InvalidTrackOneException() : base("Invalid Track One data")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTrackOneException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidTrackOneException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTrackOneException"/> class with a specified error message and a reference to the inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public InvalidTrackOneException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
