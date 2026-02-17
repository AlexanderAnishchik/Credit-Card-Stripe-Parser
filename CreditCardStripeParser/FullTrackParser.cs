
using CreditCardStripeParser.Exceptions;
using CreditCardStripeParser.Models;
using System;
using System.Linq;

namespace CreditCardStripeParser
{
    /// <summary>
    /// Parser for credit card magnetic stripe data conforming to ISO 7811-2 standard.
    /// Supports parsing Track 1 and Track 2 data with LRC validation.
    /// </summary>
    public class FullTrackParser
    {
        #region const
        private const char _SS1 = '%';
        private const char _FS1 = '^';
        private const char _ES1 = '?';
        private const char _SS2 = ';';
        private const char _FS2 = '=';
        private const char _ES2 = '?';
        #endregion

        /// <summary>
        /// Parses the full magnetic stripe track data containing both Track 1 and Track 2.
        /// </summary>
        /// <param name="fullTrack">The full track data string from the magnetic stripe reader.</param>
        /// <returns>A <see cref="FullTrackDataModel"/> containing parsed Track 1 and Track 2 data with validation status.</returns>
        /// <exception cref="InvalidTrackOneException">Thrown when Track 1 data is invalid and cannot be parsed.</exception>
        /// <exception cref="InvalidTrackTwoException">Thrown when Track 2 data is invalid and cannot be parsed.</exception>
        public FullTrackDataModel Parse(string fullTrack)
        {
            TrackOneModel track1;
            TrackTwoModel track2;
            bool isTrackOneValid;
            bool isTrackTwoValid;
            try
            {
                isTrackOneValid = _ValidateTrackOne(fullTrack);
                track1 = isTrackOneValid ? ParseTrackOne(fullTrack) : null;
            }
            catch (Exception)
            {
                throw new InvalidTrackOneException();
            }
            try
            {
                isTrackTwoValid = _ValidateTrackTwo(fullTrack);
                track2 = isTrackTwoValid ? ParseTrackTwo(fullTrack) : null;
            }
            catch (Exception)
            {
                throw new InvalidTrackTwoException();
            }
            return new FullTrackDataModel
            {
                TrackOne = track1,
                TrackTwo = track2,
                IsTrackOneValid = isTrackOneValid,
                IsTrackTwoValid = isTrackTwoValid
            };

        }
        /// <summary>
        /// Parses Track 1 data from the magnetic stripe.
        /// </summary>
        /// <param name="fullTrack">The track data string containing Track 1 data.</param>
        /// <returns>A <see cref="TrackOneModel"/> containing parsed Track 1 fields.</returns>
        /// <exception cref="Exception">Thrown when track data exceeds maximum length or parsing fails.</exception>
        public TrackOneModel ParseTrackOne(string fullTrack)
        {
            string trackString = fullTrack.Substring(1, fullTrack.IndexOf(_ES1) - 1);
            if (trackString.Length > 79) throw new Exception("Track data exced maximum length");
            string[] trackSegments = trackString.Split(_FS1);

            return new TrackOneModel
            {
                FormatCode = trackString[0],
                PAN = trackSegments[0].Substring(1),
                CardHolderName = trackSegments[1],
                ExpirationDate = new string(trackSegments[2].Take(4).ToArray()),
                ServiceCode = new string(trackSegments[2].Skip(4).Take(3).ToArray()),
                DiscretionaryData = new string(trackSegments[2].Skip(7).ToArray()),
                SourceString = fullTrack.Substring(0, fullTrack.IndexOf(_ES1)+1)
            };
        }
        /// <summary>
        /// Attempts to parse Track 1 data from the magnetic stripe without throwing exceptions.
        /// </summary>
        /// <param name="fullTrack">The track data string containing Track 1 data.</param>
        /// <param name="trackOne">When this method returns, contains the parsed Track 1 data if successful; otherwise, null.</param>
        /// <returns>true if Track 1 was successfully parsed; otherwise, false.</returns>
        public bool TryParseTrackOne(string fullTrack, out TrackOneModel trackOne)
        {
            try
            {
                if (!fullTrack.Contains(_SS1))
                {
                    trackOne = null;
                    return false;
                }
                trackOne = ParseTrackOne(fullTrack);
                return true;
            }
            catch (Exception)
            {
                trackOne = null;
                return false;
            }
        }
        /// <summary>
        /// Parses Track 2 data from the magnetic stripe.
        /// </summary>
        /// <param name="fullTrack">The track data string containing Track 2 data.</param>
        /// <returns>A <see cref="TrackTwoModel"/> containing parsed Track 2 fields.</returns>
        /// <exception cref="Exception">Thrown when track data exceeds maximum length or parsing fails.</exception>
        public TrackTwoModel ParseTrackTwo(string fullTrack)
        {

            string trackString = fullTrack.Substring(fullTrack.IndexOf(_SS2) + 1, fullTrack.LastIndexOf(_ES2) - fullTrack.IndexOf(_SS2) - 1);
            if (trackString.Length > 40) throw new Exception("Track data exced maximum length");
            string[] trackSegments = trackString.Split(_FS2);
            return new TrackTwoModel
            {
                PAN = trackSegments[0],
                ExpirationDate = new string(trackSegments[1].Take(4).ToArray()),
                ServiceCode = new string(trackSegments[1].Skip(4).Take(3).ToArray()),
                DiscretionaryData = new string(trackSegments[1].Skip(7).ToArray()),
                SourceString = fullTrack.Substring(fullTrack.IndexOf(_SS2), fullTrack.LastIndexOf(_ES2) - fullTrack.IndexOf(_SS2) + 1)
            };
        }
        /// <summary>
        /// Attempts to parse Track 2 data from the magnetic stripe without throwing exceptions.
        /// </summary>
        /// <param name="fullTrack">The track data string containing Track 2 data.</param>
        /// <param name="trackTwo">When this method returns, contains the parsed Track 2 data if successful; otherwise, null.</param>
        /// <returns>true if Track 2 was successfully parsed; otherwise, false.</returns>
        public bool TryParseTrackTwo(string fullTrack, out TrackTwoModel trackTwo)
        {
            try
            {
                if (!fullTrack.Contains(_SS2))
                {
                    trackTwo = null;
                    return false;
                }
                trackTwo = ParseTrackTwo(fullTrack);
                return true;
            }
            catch (Exception)
            {
                trackTwo = null;
                return false;
            }
        }

        #region private_methods
        /// <summary>
        /// Calculates the Longitudinal Redundancy Check (LRC) for the given byte array.
        /// </summary>
        /// <param name="bytes">The byte array to calculate LRC for.</param>
        /// <returns>The calculated LRC byte.</returns>
        private byte _CalculateLRC(byte[] bytes)
        {
            return bytes.Aggregate<byte, byte>(0, (x, y) => (byte)(x ^ y));
        }
        /// <summary>
        /// Determines whether the track data contains LRC code.
        /// </summary>
        /// <param name="fullTrack">The track data string to check.</param>
        /// <returns>true if LRC code is present; otherwise, false.</returns>
        private bool _HasLRCCode(string fullTrack)
        {
            if (fullTrack.Contains("?;") || fullTrack.EndsWith("?"))
                return false;
            return true;
        }
        /// <summary>
        /// Validates Track 1 data including LRC verification if present.
        /// </summary>
        /// <param name="fullTrack">The track data string to validate.</param>
        /// <returns>true if Track 1 is valid; otherwise, false.</returns>
        private bool _ValidateTrackOne(string fullTrack)
        {
            if (!fullTrack.Contains(_SS1)) return false;
            var es1Index = fullTrack.IndexOf(_ES1);
            if (es1Index == fullTrack.Length - 1)
                return true;
            var potentialLRC = fullTrack[es1Index + 1];
            if (potentialLRC != _SS2)
            {
                var lrc = potentialLRC;
                var calculatedLRC = _CalculateLRC(fullTrack.Substring(1, es1Index).Select(c => (byte)c).ToArray());
                if (lrc != calculatedLRC)
                    return false;
            }
            return true;

        }
        /// <summary>
        /// Validates Track 2 data including LRC verification if present.
        /// </summary>
        /// <param name="fullTrack">The track data string to validate.</param>
        /// <returns>true if Track 2 is valid; otherwise, false.</returns>
        private bool _ValidateTrackTwo(string fullTrack)
        {
            if (!fullTrack.Contains(_SS2)) return false;
            var potentialLRC = fullTrack.Last();
            if (potentialLRC != _ES2)
            {
                var lrc = potentialLRC;
                var calculatedLRC = _CalculateLRC(fullTrack.Substring(fullTrack.IndexOf(_SS2) + 1, fullTrack.LastIndexOf(_ES2) - fullTrack.IndexOf(_SS2)).Select(c => (byte)c).ToArray());
                if (lrc != calculatedLRC)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
