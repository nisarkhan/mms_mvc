using System;
using System.Linq;

namespace Rhml.Mms.Web.Utility
{
    /// <summary> Utility for detecting wildcard searches.
    /// Search type determined from LikePatternMatch
    /// </summary>
    public enum LikePatternSearchType
    {
        /// <summary> Get all elements (no pattern selection) </summary>
        All,
        /// <summary> Search for all elements that start with the entered pattern </summary>
        StartsWith,
        /// <summary> Get all elements that end with the entered pattern </summary>
        EndsWith,
        /// <summary> Get all elements that exactly match the entered pattern </summary>
        Full,
        /// <summary> Get all elements that contain the search pattern </summary>
        Contains
    }

    /// <summary> Utility to help generate search queries that are similar to SQL LIKE queries
    /// </summary>
    public class LikePatternMatch
    {
        #region data
        private static readonly char _matchChar = '*';
        private string _matchString;
        private bool _hasStartWildCard;
        private bool _hasEndWildCard;
        private bool _isEmpty;
        #endregion


        #region properties
        /// <summary> Return type of search to run
        /// </summary>
        /// <value>
        /// The type of the search to use
        /// </value>
        public LikePatternSearchType SearchType
        {
            get
            {
                if (_isEmpty)
                {
                    return LikePatternSearchType.All;
                }
                if (_hasStartWildCard)
                {
                    return _hasEndWildCard ? LikePatternSearchType.Contains :
                                             LikePatternSearchType.EndsWith;
                }
                return _hasEndWildCard ? LikePatternSearchType.StartsWith :
                                         LikePatternSearchType.Full;
            }
        }

        /// <summary> Gets the search string to use for the indicated query type
        /// </summary>
        public string SearchString
        {
            get { return _matchString; }
        }
        #endregion


        #region constructor
        /// <summary> Initializes a new instance of the <see cref="LikePatternMatch"/> class.
        /// </summary>
        /// <param name="matchPattern">The match pattern.</param>
        public LikePatternMatch(string matchPattern)
        {
            _matchString = matchPattern;
            Analyze(matchPattern);
        }
        #endregion


        #region private
        private void Analyze(string pattern)
        {
            pattern = pattern.Trim();
            int length = pattern != null ? pattern.Length : 0;
            int start = 0;

            if (string.IsNullOrWhiteSpace(pattern) || length < 1)
            {
                pattern = string.Empty;
                _isEmpty = true;
                return;
            }
            _hasStartWildCard = pattern[0] == _matchChar;
            _hasEndWildCard = pattern[length - 1] == _matchChar;

            if (_hasEndWildCard)
            {
                length--;
            }
            if (_hasStartWildCard)
            {
                length--;
                start++;
            }
            if (length < 0) length = 0;
            _matchString = pattern.Substring(start, length);
        }
        #endregion
    }
}