using CodingChallenge.Application;
using CodingChallenge.Domain;
using InfoTrack.Assignment.Domain;
using Xunit;

namespace CodingChallenge.UnitTests
{
    public class SearchProviderTests
    {
        [Fact]
        public void GivenGoogleTypeTheProviderSearchWithType()
        {
            var searchProvider  = new SearchEngineProvider();
            var searchEngine = searchProvider.GetSearchStrategy(SearchEngineType.Google, new SearchConfiguration(){MaxSearchResult = 20});
            Assert.IsType<GoogleSearchStrategy>(searchEngine);
        }

        [Fact]
        public void GivenBingTypeTheProviderSearchWithType()
        {
            var searchProvider = new SearchEngineProvider();
            var searchEngine = searchProvider.GetSearchStrategy(SearchEngineType.Bing, new SearchConfiguration() { MaxSearchResult = 20 });
            Assert.IsType<BingSearchStrategy>(searchEngine);
        }
    }
}
