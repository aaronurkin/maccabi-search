using MaccabiSearch.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaccabiSearch.Api.Controllers
{
    [ApiController]
    public class RestApiControllerBase : ControllerBase
    {
        private readonly IDictionary<ServiceResultStatus, int> statuses;

        public RestApiControllerBase(IDictionary<ServiceResultStatus, int> statuses)
        {
            this.statuses = statuses ?? throw new ArgumentNullException(nameof(statuses));
        }

        protected IActionResult ResolveResponse(IServiceResult result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            var statusCode = statuses[result.Status];

            if (result.Data == null)
            {
                return StatusCode(statusCode);
            }

            return StatusCode(statusCode, result.Data);
        }
    }
}
