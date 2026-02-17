namespace CreditCardStripeParser.Models
{
    /// <summary>
    /// Represents the complete magnetic stripe data containing both Track 1 and Track 2 information.
    /// </summary>
    public class FullTrackDataModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether Track 1 data is valid.
        /// </summary>
        public bool IsTrackOneValid { get; set; }
        
        /// <summary>
        /// Gets or sets the parsed Track 1 data. Will be null if Track 1 is not valid or not present.
        /// </summary>
        public TrackOneModel TrackOne { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether Track 2 data is valid.
        /// </summary>
        public bool IsTrackTwoValid { get; set; }
        
        /// <summary>
        /// Gets or sets the parsed Track 2 data. Will be null if Track 2 is not valid or not present.
        /// </summary>
        public TrackTwoModel TrackTwo { get; set; }
    }
}
