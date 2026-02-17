namespace CreditCardStripeParser.Models
{
    /// <summary>
    /// ISO 7811-2 track one character encoding definition:
    /// SS FC PAN FS Name FS Date Discretionary Data ES LRC
    /// </summary>
    public class TrackOneModel
    {
        /// <summary>
        /// Gets or sets the format code (typically 'B').
        /// </summary>
        public char FormatCode { get; set; }
        
        /// <summary>
        /// Gets or sets the Primary Account Number (card number).
        /// </summary>
        public string PAN { get; set; }
        
        /// <summary>
        /// Gets or sets the cardholder's name as encoded on the magnetic stripe.
        /// </summary>
        public string CardHolderName { get; set; }
        
        /// <summary>
        /// Gets or sets the expiration date in YYMM format.
        /// </summary>
        public string ExpirationDate { get; set; }
        
        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        public string ServiceCode { get; set; }
        
        /// <summary>
        /// Gets or sets the discretionary data field.
        /// </summary>
        public string DiscretionaryData { get; set; }
        
        /// <summary>
        /// Gets or sets the original source string that was parsed.
        /// </summary>
        public string SourceString { get; set; }
    }
}
