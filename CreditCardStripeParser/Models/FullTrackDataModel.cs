namespace CreditCardStripeParser.Models
{
    /// <summary>
    /// Represents the complete parsed data from both magnetic stripe tracks.
    /// </summary>
    public class FullTrackDataModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether Track 1 data passed validation.
        /// </summary>
        public bool IsTrackOneValid { get; set; }
        
        /// <summary>
        /// Gets or sets the parsed Track 1 data, or null if Track 1 was invalid.
        /// </summary>
        public TrackOneModel TrackOne { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether Track 2 data passed validation.
        /// </summary>
        public bool IsTrackTwoValid { get; set; }
        
        /// <summary>
        /// Gets or sets the parsed Track 2 data, or null if Track 2 was invalid.
        /// </summary>
        public TrackTwoModel TrackTwo { get; set; }
    }
}
