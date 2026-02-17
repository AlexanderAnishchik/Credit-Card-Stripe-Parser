namespace CreditCardStripeParser.Models
{
    /// <summary>
    /// ISO 7811-2 track one character encoding definition:
    /// SS FC PAN FS Name FS Date Discretionary Data ES LRC
    /// </summary>
    public class TrackOneModel
    {
        /// <summary>
        /// Gets or sets the format code (typically 'A' or 'B').
        /// </summary>
        public char FormatCode { get; set; }
        
        /// <summary>
        /// Gets or sets the Primary Account Number (PAN) - the card number.
        /// </summary>
        public string PAN { get; set; }
        
        /// <summary>
        /// Gets or sets the cardholder's name as encoded on the magnetic stripe.
        /// </summary>
        public string CardHolderName { get; set; }
        
        /// <summary>
        /// Gets or sets the card expiration date in YYMM format.
        /// </summary>
        public string ExpirationDate { get; set; }
        
        /// <summary>
        /// Gets or sets the service code (3 digits) that defines the card usage restrictions.
        /// </summary>
        public string ServiceCode { get; set; }
        
        /// <summary>
        /// Gets or sets the discretionary data section containing additional information.
        /// </summary>
        public string DiscretionaryData { get; set; }
        
        /// <summary>
        /// Gets or sets the original source string from which this track was parsed.
        /// </summary>
        public string SourceString { get; set; }
    }
}
