using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CodingChallenge.Domain;
using CodingChallenge.Domain.Interfaces;

namespace CodingChallenge.Application
{
    public class GoogleSearchStrategy : SearchStrategyBase, ISearchStrategy
    {
        public GoogleSearchStrategy(string searchEngineUrl, int maxSearchRecords):base(searchEngineUrl, maxSearchRecords)
        {
            
        }

        protected override List<int> GetLinkCountFromResponse(SearchContext context, string htmlString)
        {
            var index = 1;
            var result = new List<int>();
            var siteLinkPattern = new Regex(@"<a[ ]href=""[/]url[?]q=(.*?)""[^>]*?>.*?<[/]a>", RegexOptions.Singleline);

            var match = siteLinkPattern.Matches(htmlString);
            foreach (Match m in match)
            {
                var isMatch = m.ToString().Contains(context.SearchInput.Site, StringComparison.InvariantCultureIgnoreCase);

                if (isMatch)
                {
                    result.Add(index);
                }

                index++;
            }

            return result;
        }
    }
}
