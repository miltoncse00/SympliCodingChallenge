using System.ComponentModel.DataAnnotations;
using CodingChallenge.Application;
using CodingChallenge.Domain;
using NSubstitute;
using NSubstitute.Extensions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace CodingChallenge.UnitTests
{
    public class BingSearchEngineTests
    {
        [Fact]
        public async Task GivenTheServiceAllPageReturningValueVerifyTheValueForSympli()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"BingSearchResult10.html");
            string htmlString = File.ReadAllText(path);
            var bingSearchStrategy = Substitute.ForPartsOf<BingSearchStrategy>(string.Empty, 10);
            bingSearchStrategy.Configure().GetType().GetMethod("DownloadPageHtml", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(bingSearchStrategy, new object[1]{Arg.Any<SearchContext>()}).Returns(Task.FromResult(htmlString));
            var result = await bingSearchStrategy.Search(new SearchContext()
                {SearchInput = new SearchInput {Site = "sympli.com" } });
            
            Assert.Equal("1",result.Positions);
        }

        [Fact]
        public async Task GivenTheServiceAllPageReturningValueVerifyTheValueForUnkownUrl()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"BingSearchResult10.html");
            string htmlString = File.ReadAllText(path);
            var bingSearchStrategy = Substitute.ForPartsOf<BingSearchStrategy>(string.Empty, 10);
            bingSearchStrategy.Configure().GetType().GetMethod("DownloadPageHtml", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(bingSearchStrategy, new object[1] { Arg.Any<SearchContext>() }).Returns(Task.FromResult(htmlString));
            var result = await bingSearchStrategy.Search(new SearchContext()
                {  SearchInput = new SearchInput { Site = "unknown.com.au" } });
            Assert.Equal( "No Match Records", result.Positions);
        }
    }
}
