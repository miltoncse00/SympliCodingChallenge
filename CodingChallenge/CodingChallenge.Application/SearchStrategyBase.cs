using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CodingChallenge.Domain;

namespace CodingChallenge.Application
{
    public abstract class SearchStrategyBase
    {
        private const string NoMatchRecords = "No Match Records";
        protected string SearchEngineUrl;
        protected int MaxSearchRecords;

        protected SearchStrategyBase(string searchEngineUrl, int maxSearchRecords)
        {
            SearchEngineUrl = searchEngineUrl;
            MaxSearchRecords = maxSearchRecords;
        }

        public async Task<SearchResult> Search(SearchContext context)
        {
            var results = await SearchPage(context);
            
            var search = new SearchResult();
            search.SearchEngineType = context.SearchInput.SearchEngineType;

            if (results.Count ==0)
            {
                search.Positions = NoMatchRecords;
                return search;
            }

            search.Positions = string.Join<int>(",", results);
            
            return search;
        }

        protected virtual async Task<List<int>> SearchPage(SearchContext context)
        {
            var htmlString = await DownloadPageHtml(context);

            var result = GetLinkCountFromResponse(context, htmlString);

            return result;
        }

        protected virtual async Task<string> DownloadPageHtml(SearchContext context)
        {
            var searchPage = string.Format(SearchEngineUrl, context.SearchInput.Keyword, MaxSearchRecords);
            using var webClient = new WebClient();
            var htmlString = await webClient.DownloadStringTaskAsync(searchPage);
            return htmlString;
        }

        protected abstract List<int> GetLinkCountFromResponse(SearchContext context, string htmlString);
    }
}