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
    public class GoogleSearchEngineTests
    {
        [Fact]
        public async Task GivenTheServiceAllPageReturningValueVerifyTheValueForSympli()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GoogleSearchResult10.html");
            string htmlString = File.ReadAllText(path);
            var googleSearchStrategy = Substitute.ForPartsOf<GoogleSearchStrategy>(string.Empty, 10);
            googleSearchStrategy.Configure().GetType().GetMethod("DownloadPageHtml", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(googleSearchStrategy, new object[1]{Arg.Any<SearchContext>()}).Returns(Task.FromResult(htmlString));
            var result = await googleSearchStrategy.Search(new SearchContext()
                {SearchInput = new SearchInput {Site = "sympli.com" } });
            
            Assert.Equal("2,3",result.Positions);
        }

        [Fact]
        public async Task GivenTheServiceAllPageReturningValueVerifyTheValueForUnkownUrl()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GoogleSearchResult10.html");
            string htmlString = File.ReadAllText(path);
            var googleSearchStrategy = Substitute.ForPartsOf<GoogleSearchStrategy>(string.Empty, 10);
            googleSearchStrategy.Configure().GetType().GetMethod("DownloadPageHtml", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(googleSearchStrategy, new object[1] { Arg.Any<SearchContext>() }).Returns(Task.FromResult(htmlString));
            var result = await googleSearchStrategy.Search(new SearchContext()
                {  SearchInput = new SearchInput { Site = "unknown.com.au" } });
            Assert.Equal( "No Match Records", result.Positions);
        }

        [Fact]
        public async Task GivenTheServiceAllPageReturningValueHandleExceptionWithEmptySiteValue()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"GoogleSearchResult10.html");
            string htmlString = File.ReadAllText(path);
            var googleSearchStrategy = Substitute.ForPartsOf<GoogleSearchStrategy>(string.Empty, 10);
            googleSearchStrategy.Configure().GetType().GetMethod("DownloadPageHtml", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.Invoke(googleSearchStrategy, new object[1] { Arg.Any<SearchContext>() }).Returns(Task.FromResult(htmlString));
            Assert.ThrowsAsync<ValidationException>(async ()=>await googleSearchStrategy.Search(new SearchContext()
            {  SearchInput = new SearchInput { Site = "" } }));
        }
    }
}
