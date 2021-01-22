namespace CodingChallenge.Domain
{
    public class SearchInput
    {
        public string Keyword { get; set; }
        public string Site { get; set; }
        public SearchEngineType SearchEngineType { get; set; }
    }
}
