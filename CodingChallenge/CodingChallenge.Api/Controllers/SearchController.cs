using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using CodingChallenge.Application;
using CodingChallenge.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Api.Controllers
{
    [Route("api/Search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<ActionResult> Search( SearchInput searchInput)
        {
            
                var result = await _searchService.Search(searchInput);
                return Ok(result);
        }
    }
}
