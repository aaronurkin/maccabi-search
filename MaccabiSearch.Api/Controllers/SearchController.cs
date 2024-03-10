using MaccabiSearch.Application.Services;
using MaccabiSearch.Common.Models;
using MaccabiSearch.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaccabiSearch.Api.Controllers
{
    [Route("[controller]")]
    public class SearchController : RestApiControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService, IDictionary<ServiceResultStatus, int> statuses)
            : base(statuses)
        {
            this.searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SearchDto requestData)
        {
            var result = await searchService.Search(requestData);
            var response = ResolveResponse(result);
            return response;
        }
    }
}
