namespace CreditCardStripeParser.Models
{
    /// <summary>
    /// ISO 7811-2 Track Two encoding definition:
    /// SS PAN FS Date SVC CD Discretionary Data ES LRC
    /// </summary>
    public class TrackTwoModel
    {
        public string PAN { get; set; }
        public string ExpirationDate { get; set; }
        public string ServiceCode { get; set; }
        public string DiscretionaryData { get; set; }
        public string SourceString { get; set; }

    }
}
