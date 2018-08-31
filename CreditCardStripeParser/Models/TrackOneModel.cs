namespace CreditCardStripeParser.Models
{
    /// <summary>
    /// ISO 7811-2 track one character encoding definition:
    /// SS FC PAN FS Name FS Date Discretionary Data ES LRC
    /// </summary>
    public class TrackOneModel
    {

        public char FormatCode { get; set; }
        public string PAN { get; set; }
        public string CardHolderName { get; set; }
        public string ExpirationDate { get; set; }
        public string ServiceCode { get; set; }
        public string DiscretionaryData { get; set; }
        public string SourceString { get; set; }
    }
}
