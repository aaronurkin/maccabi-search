using MaccabiSearch.Common.Models;
using System.Text.Json;

namespace MaccabiSearch.Infrastructure.Services.Implementations
{
    /// <summary>
    /// Default Resolver of Failed Search Engine API Response.
    /// </summary>
    public class GoogleSearchDefaultFailedResponseResolver : IGoogleSearchResponseResolver
    {
        /// <summary>
        /// Resolves a failed response from the Google Search Engine API.
        /// </summary>
        /// <param name="response">The response from the Google Search Engine API.</param>
        /// <returns>Failed result.</returns>
        public async Task<IServiceResult> Resolve(HttpResponseMessage response)
        {
            var resultData = default(IDictionary<string, object>);

            var content = response.Content == null
                ? default(string)
                : await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(content))
            {
                resultData = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
            }

            var result = new ServiceResult<IDictionary<string, object>>(resultData, ServiceResultStatus.Failed);
            return result;
        }
    }
}
