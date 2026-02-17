namespace CreditCardStripeParser.Models
{
    /// <summary>
    /// ISO 7811-2 Track Two encoding definition:
    /// SS PAN FS Date SVC CD Discretionary Data ES LRC
    /// </summary>
    public class TrackTwoModel
    {
        /// <summary>
        /// Gets or sets the Primary Account Number (PAN) - the card number.
        /// </summary>
        public string PAN { get; set; }
        
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
