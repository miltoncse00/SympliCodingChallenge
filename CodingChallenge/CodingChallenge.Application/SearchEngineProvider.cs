using CodingChallenge.Domain;
using CodingChallenge.Domain.Interfaces;
using InfoTrack.Assignment.Domain;

namespace CodingChallenge.Application
{
    public class SearchEngineProvider: ISearchEngineProvider
    {
        public ISearchStrategy GetSearchStrategy(SearchEngineType searchEngineType, SearchConfiguration searchConfiguration)
        {
            switch (searchEngineType)
            {
                case SearchEngineType.Google:
                    return new GoogleSearchStrategy(searchConfiguration.GoogleSearchUrl, searchConfiguration.MaxSearchResult);
                case SearchEngineType.Bing:
                    return new BingSearchStrategy(searchConfiguration.BingSearchUrl, searchConfiguration.MaxSearchResult);
                default:
                    return new GoogleSearchStrategy(searchConfiguration.GoogleSearchUrl, searchConfiguration.MaxSearchResult);
            }
        }
    }
}
