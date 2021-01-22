using System;
using System.Threading.Tasks;
using CodingChallenge.Domain;
using CodingChallenge.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CodingChallenge.Application
{
    public interface ISearchService
    {
        Task<SearchResult> Search(SearchInput searchInput);
    }

    public class SearchService : ISearchService
    {
        private readonly SearchConfiguration _searchConfig;
        private readonly ISearchEngineProvider _provider;
        private readonly IMemoryCache _cache;
        private readonly CacheConfiguration _cacheConfiguration;
        public SearchService(IOptions<SearchConfiguration> searchOptions, ISearchEngineProvider provider, IMemoryCache cache, IOptions<CacheConfiguration> cacheOptions)
        {
            _searchConfig = searchOptions.Value;
            _provider = provider;
            _cache = cache;
            _cacheConfiguration = cacheOptions.Value;
        }

        public async Task<SearchResult> Search(SearchInput searchInput)
        {
            string cacheKey = $"{searchInput.Keyword}_{searchInput.Site}_{searchInput.SearchEngineType}";
            if (!_cache.TryGetValue(cacheKey, out SearchResult searchResult))
            {
                var searchStrategy =
                    _provider.GetSearchStrategy(searchInput.SearchEngineType, _searchConfig);

                searchResult = await searchStrategy.Search(new SearchContext
                    {SearchInput = searchInput});

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheConfiguration.MaxTimeMins)
                };

                _cache.Set(cacheKey, searchResult, cacheEntryOptions);
            }

            return searchResult;
        }
    }
}
