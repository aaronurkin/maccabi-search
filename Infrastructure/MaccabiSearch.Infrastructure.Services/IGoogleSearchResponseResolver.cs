using MaccabiSearch.Common.Models;

namespace MaccabiSearch.Infrastructure.Services
{
    /// <summary>
    /// Contract of the Google Search Engine response resolver.
    /// </summary>
    public interface IGoogleSearchResponseResolver
    {
        /// <summary>
        /// Resolves Succeeded result from the Google Search engine response.
        /// </summary>
        /// <param name="response">The response to resolve result from.</param>
        /// <returns>Service result based on the response status.</returns>
        Task<IServiceResult> Resolve(HttpResponseMessage response);
    }
}
