using MaccabiSearch.Common.Models;
using MaccabiSearch.Common.Services;
using MaccabiSearch.Domain.Models;
using MaccabiSearch.Infrastructure.Models.GoogleSearchEngine;
using System.Text.Json;

namespace MaccabiSearch.Infrastructure.Services.Implementations
{
    /// <summary>
    /// Resolver of the Succeeded response from the Google Search Engine
    /// </summary>
    public class GoogleSearchOkResponseResolver : IGoogleSearchResponseResolver
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;
        private readonly IModelMapper<GoogleSearchEngineResponse, IEnumerable<SearchResult>> mapper;

        public GoogleSearchOkResponseResolver(
            JsonSerializerOptions jsonSerializerOptions,
            IModelMapper<GoogleSearchEngineResponse, IEnumerable<SearchResult>> mapper
        )
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.jsonSerializerOptions = jsonSerializerOptions ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));
        }

        /// <summary>
        /// Resolves Succeeded result from the Google Search engine response
        /// </summary>
        /// <param name="response">The response to resolve result from.</param>
        /// <returns>Succeeded result</returns>
        public async Task<IServiceResult> Resolve(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var gseResponse  = JsonSerializer.Deserialize<GoogleSearchEngineResponse>(content, jsonSerializerOptions);
            var searchResults = mapper.Map(gseResponse!);
            var result = new ServiceResult<IEnumerable<SearchResult>>(searchResults, ServiceResultStatus.Succeeded);

            return result;
        }
    }
}
