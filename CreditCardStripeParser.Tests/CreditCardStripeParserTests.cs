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

        [Theory]
        [InlineData(_testTrackOne)]
        [InlineData(_testFullTrack)]
        [InlineData(_testTrackOneLRC)]
        [InlineData(_testFullTrackLRC)]
        public void TryParseTrackOne_Should_Return_True_For_Valid_Track(string track)
        {
            var parser = new FullTrackParser();
            var result = parser.TryParseTrackOne(track, out var trackOne);
            
            Assert.True(result);
            Assert.NotNull(trackOne);
            Assert.Equal("5168755544412233", trackOne.PAN);
            Assert.Equal("PKMMV/UNEMBOXXXX          ", trackOne.CardHolderName);
            Assert.Equal("1807", trackOne.ExpirationDate);
            Assert.Equal("111", trackOne.ServiceCode);
        }

        [Theory]
        [InlineData("invalid track data")]
        [InlineData(";5168755544412233=18071111000011100000?")]  // Only Track 2
        [InlineData("")]
        public void TryParseTrackOne_Should_Return_False_For_Invalid_Track(string track)
        {
            var parser = new FullTrackParser();
            var result = parser.TryParseTrackOne(track, out var trackOne);
            
            Assert.False(result);
            Assert.Null(trackOne);
        }

        [Theory]
        [InlineData(_testTrackTwo)]
        [InlineData(_testFullTrack)]
        [InlineData(_testTrackTwoLRC)]
        [InlineData(_testFullTrackLRC)]
        public void TryParseTrackTwo_Should_Return_True_For_Valid_Track(string track)
        {
            var parser = new FullTrackParser();
            var result = parser.TryParseTrackTwo(track, out var trackTwo);
            
            Assert.True(result);
            Assert.NotNull(trackTwo);
            Assert.Equal("5168755544412233", trackTwo.PAN);
            Assert.Equal("1807", trackTwo.ExpirationDate);
            Assert.Equal("111", trackTwo.ServiceCode);
        }

        [Theory]
        [InlineData("invalid track data")]
        [InlineData("%B5168755544412233^PKMMV/UNEMBOXXXX          ^1807111100000000000000111000000?")]  // Only Track 1
        [InlineData("")]
        public void TryParseTrackTwo_Should_Return_False_For_Invalid_Track(string track)
        {
            var parser = new FullTrackParser();
            var result = parser.TryParseTrackTwo(track, out var trackTwo);
            
            Assert.False(result);
            Assert.Null(trackTwo);
        }

        [Fact]
        public void ParseTrackOne_Should_Parse_Correctly()
        {
            var parser = new FullTrackParser();
            var result = parser.ParseTrackOne(_testTrackOne);
            
            Assert.NotNull(result);
            Assert.Equal('B', result.FormatCode);
            Assert.Equal("5168755544412233", result.PAN);
            Assert.Equal("PKMMV/UNEMBOXXXX          ", result.CardHolderName);
            Assert.Equal("1807", result.ExpirationDate);
            Assert.Equal("111", result.ServiceCode);
            Assert.Equal("100000000000000111000000", result.DiscretionaryData);
        }

        [Fact]
        public void ParseTrackTwo_Should_Parse_Correctly()
        {
            var parser = new FullTrackParser();
            var result = parser.ParseTrackTwo(_testTrackTwo);
            
            Assert.NotNull(result);
            Assert.Equal("5168755544412233", result.PAN);
            Assert.Equal("1807", result.ExpirationDate);
            Assert.Equal("111", result.ServiceCode);
            Assert.Equal("1000011100000", result.DiscretionaryData);
        }

        [Fact]
        public void Parse_Should_Handle_Track_Without_LRC()
        {
            var parser = new FullTrackParser();
            var result = parser.Parse(_testFullTrack);
            
            Assert.NotNull(result);
            Assert.True(result.IsTrackOneValid);
            Assert.True(result.IsTrackTwoValid);
            Assert.NotNull(result.TrackOne);
            Assert.NotNull(result.TrackTwo);
        }

        [Fact]
        public void Parse_Should_Handle_Track_With_LRC()
        {
            var parser = new FullTrackParser();
            var result = parser.Parse(_testFullTrackLRC);
            
            Assert.NotNull(result);
            Assert.True(result.IsTrackOneValid);
            Assert.True(result.IsTrackTwoValid);
            Assert.NotNull(result.TrackOne);
            Assert.NotNull(result.TrackTwo);
        }

        [Fact]
        public void Parse_Should_Set_IsTrackOneValid_False_When_Track_One_Missing()
        {
            var parser = new FullTrackParser();
            var result = parser.Parse(_testTrackTwo);
            
            Assert.False(result.IsTrackOneValid);
            Assert.Null(result.TrackOne);
        }

        [Fact]
        public void Parse_Should_Set_IsTrackTwoValid_False_When_Track_Two_Missing()
        {
            var parser = new FullTrackParser();
            var result = parser.Parse(_testTrackOne);
            
            Assert.False(result.IsTrackTwoValid);
            Assert.Null(result.TrackTwo);
        }
    }
}

