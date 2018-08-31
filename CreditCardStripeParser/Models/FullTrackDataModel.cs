namespace CreditCardStripeParser.Models
{
    public class FullTrackDataModel
    {
        public bool IsTrackOneValid { get; set; }
        public TrackOneModel TrackOne { get; set; }
        public bool IsTrackTwoValid { get; set; }
        public TrackTwoModel TrackTwo { get; set; }
    }
}
