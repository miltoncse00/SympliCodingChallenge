namespace CodingChallenge.Domain
{
    public class SearchConfiguration
    {
        public const string Setting = "SearchOptions";
        public string GoogleSearchUrl { get; set; }
        public string BingSearchUrl { get; set; }
        public int MaxSearchResult { get; set; }
    }
}
