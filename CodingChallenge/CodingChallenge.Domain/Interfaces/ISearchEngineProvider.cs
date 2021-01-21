using InfoTrack.Assignment.Domain;

namespace CodingChallenge.Domain.Interfaces
{
    public interface ISearchEngineProvider
    {
        ISearchStrategy GetSearchStrategy (SearchEngineType searchEngineType, SearchConfiguration searchConfiguration);
    }
}