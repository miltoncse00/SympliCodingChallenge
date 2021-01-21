using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CodingChallenge.Domain;
using CodingChallenge.Domain.Interfaces;
using InfoTrack.Assignment.Domain;
using Microsoft.Extensions.Options;

namespace CodingChallenge.Application
{
    public interface ISearchService
    {
        Task<List<SearchResult>> Search(SearchInput searchInput);
    }

    public class SearchService : ISearchService
    {
        private const string InvalidInputUserMustProvideSiteValueForSearch = "Invalid input.User must provide Site value for search";
        private readonly SearchConfiguration _searchConfig;
        private readonly ISearchEngineProvider _provider;

        public SearchService(IOptions<SearchConfiguration> searchOptions, ISearchEngineProvider provider)
        {
            _searchConfig = searchOptions.Value;
            _provider = provider;
        }

        public async Task<List<SearchResult>> Search(SearchInput searchInput)
        {
            if(searchInput == null || string.IsNullOrWhiteSpace(searchInput.Site))
                throw new ValidationException(InvalidInputUserMustProvideSiteValueForSearch);

            var searchResults = new List<SearchResult>();
            var searchStrategy =
                _provider.GetSearchStrategy(searchInput.SearchEngineType, _searchConfig);

            var infoTrackSearchResult = await searchStrategy.Search(new SearchContext
            { SearchInput = searchInput});

            searchResults.Add(infoTrackSearchResult);

            return searchResults;
        }
    }
}
