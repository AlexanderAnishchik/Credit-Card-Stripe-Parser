# Credit Card Magnetic Stripe Parser
Library for parsing magnetic stripe track data of credit cards

[NuGet CreditCardStripeParser Package](https://www.nuget.org/packages/CreditCardStripeParser/1.0.0)

### A simple .Net Standard parser with following functionality:

* Parse whole magnetic stripe from a card reader
* Parse whole magnetic stripe with LRC codes after Track 1 or Track 2
* Separately validate and parse Track 1, Track 2
* Calculate and validate LRC to confirm the integrity of the data

### Models

```

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
    
     public class FullTrackDataModel
    {
        public bool IsTrackOneValid { get; set; }
        public TrackOneModel TrackOne { get; set; }
        public bool IsTrackTwoValid { get; set; }
        public TrackTwoModel TrackTwo { get; set; }
    }
}
```

### Example Code:

```
private String _testFullTrack = "%B5168755544412233^PKMMV/UNEMBOXXXX       ^1807111100000000000000111000000?;5168755544412233=18071111000011100000?";
 var result = new FullTrackParser().ParseTrackOne(_testFullTrack);
 ```
 will be parsed into a model:
 
  ```
TrackOneModel testTrack1 = new TrackOneModel
            {
                FormatCode = 'B',
                PAN = "5168755544412233",
                CardHolderName = "PKMMV/UNEMBOXXXX          ",
                ExpirationDate = "1807",
                ServiceCode = "111",
                DiscretionaryData = "100000000000000111000000",
                SourceString = "B5168755544412233^PKMMV/UNEMBOXXXX          ^1807111100000000000000111000000"
            };
 ```
