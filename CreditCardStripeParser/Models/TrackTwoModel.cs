namespace CreditCardStripeParser.Models
{
    /// <summary>
    /// ISO 7811-2 Track Two encoding definition:
    /// SS PAN FS Date SVC CD Discretionary Data ES LRC
    /// </summary>
    public class TrackTwoModel
    {
        /// <summary>
        /// Gets or sets the Primary Account Number (card number).
        /// </summary>
        public string PAN { get; set; }
        
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
