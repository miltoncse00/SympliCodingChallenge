using System.Threading.Tasks;

namespace CodingChallenge.Domain.Interfaces
{
    public interface ISearchStrategy
    {
        Task<SearchResult> Search(SearchContext context);
    }
}