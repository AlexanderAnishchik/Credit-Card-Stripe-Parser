# Credit Card Magnetic Stripe Parser
Library for parsing magnetic stripe track data of credit cards

### A simple .Net Standard parser with following functionality:

* Parse whole magnetic stripe from card reader
* Parse whole magnetic stripe with LRC codes after Track 1 or Track 2
* Separately validate and parse Track 1, Track 2
* Calculate and validate LRC to confirm the integrity of the data

### Example Code:

```
private String _testFullTrack = "%B5168755544412233^PKMMV/UNEMBOXXXX       ^1807111100000000000000111000000?;5168755544412233=18071111000011100000?";
 var result = new FullTrackParser().Parse(_testFullTrack);
 ```
 will be parse into model:
 
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
