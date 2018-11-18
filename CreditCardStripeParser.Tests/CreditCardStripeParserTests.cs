using CreditCardStripeParser.Models;
using Newtonsoft.Json;
using System;
using Xunit;

namespace CreditCardStripeParser.Tests
{
    public class CreditCardStripeParserWithoutLRCTests
    {
        private const String _testFullTrack = "%B5168755544412233^PKMMV/UNEMBOXXXX          ^1807111100000000000000111000000?;5168755544412233=18071111000011100000?";
        private const String _testFullTrackLRC = "%B5168755544412233^PKMMV/UNEMBOXXXX          ^1807111100000000000000111000000?3;5168755544412233=18071111000011100000?\0";

        private const String _testTrackOne = "%B5168755544412233^PKMMV/UNEMBOXXXX          ^1807111100000000000000111000000?";
        private const String _testTrackOneLRC = "%B5168755544412233^PKMMV/UNEMBOXXXX          ^1807111100000000000000111000000?3";

        private const String _testTrackTwo = ";5168755544412233=18071111000011100000?";
        private const String _testTrackTwoLRC = ";5168755544412233=18071111000011100000?\0";

        [Theory]
        [InlineData(_testFullTrack)]
        [InlineData(_testFullTrackLRC)]
        public void Should_Parse_Full_Track_Without_Exception(string track)
        {
            var parser = new FullTrackParser();
            var result = parser.Parse(track);
            Assert.True(result.IsTrackOneValid && result.IsTrackTwoValid);
        }
        [Theory]
        [InlineData(_testFullTrack)]
        [InlineData(_testTrackOne)]
        [InlineData(_testFullTrackLRC)]
        [InlineData(_testTrackOneLRC)]
        public void Should_TrackOneModel_Match_Track1_String(string track)
        {
            TrackOneModel testTrack1 = new TrackOneModel
            {
                FormatCode = 'B',
                PAN = "5168755544412233",
                CardHolderName = "PKMMV/UNEMBOXXXX          ",
                ExpirationDate = "1807",
                ServiceCode = "111",
                DiscretionaryData = "100000000000000111000000",
                SourceString = "%B5168755544412233^PKMMV/UNEMBOXXXX          ^1807111100000000000000111000000?"
            };
            var parser = new FullTrackParser();
            var result = parser.Parse(track);
            Assert.Equal(JsonConvert.SerializeObject(testTrack1), JsonConvert.SerializeObject(result.TrackOne));
        }
        [Theory]
        [InlineData(_testFullTrack)]
        [InlineData(_testTrackTwo)]
        [InlineData(_testFullTrackLRC)]
        [InlineData(_testTrackTwoLRC)]
        public void Should_TrackTwoModel_Match_Track2_String(string track)
        {
            TrackTwoModel testTrack2 = new TrackTwoModel
            {
                PAN = "5168755544412233",
                ExpirationDate = "1807",
                ServiceCode = "111",
                DiscretionaryData = "1000011100000",
                SourceString = ";5168755544412233=18071111000011100000?"
            };
            var parser = new FullTrackParser();
            var result = parser.Parse(track);
            Assert.Equal(JsonConvert.SerializeObject(testTrack2), JsonConvert.SerializeObject(result.TrackTwo));
        }
        [Fact]
        public void Should_Throw_Exception_On_Invalid_Track()
        {
            var str = "423jobhjp843hp389h aiajge84h pt394q : 'weg;43g";
            var parser = new FullTrackParser();
            Assert.ThrowsAny<Exception>(() => parser.Parse(str));
        }
    }
}

