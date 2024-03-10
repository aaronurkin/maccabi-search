using MaccabiSearch.Common.Models;
using MaccabiSearch.Common.Services;
using MaccabiSearch.Infrastructure.Models;

namespace MaccabiSearch.Infrastructure.Services.Implementations
{
    /// <summary>
    /// An API client of the Google Search Engine.
    /// </summary>
    public class GoogleSimpleSearchApiClient : ISearchEngineApiClient
    {
        private readonly HttpClient http;
        private readonly IServiceResolver serviceResolver;
        private readonly IGoogleSimpleSearchApiClientOptions options;

        public GoogleSimpleSearchApiClient(
            HttpClient http,
            IServiceResolver serviceResolver,
            IGoogleSimpleSearchApiClientOptions options
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.serviceResolver = serviceResolver ?? throw new ArgumentNullException(nameof(serviceResolver));
        }

        /// <summary>
        /// Retrieving results from the Google Search Engine API.
        /// </summary>
        /// <param name="data">Search parameters.</param>
        /// <returns>A service result instance containing search results if the search succeeded.</returns>
        public async Task<IServiceResult> Search(SearchDto data)
        {
            var pageSize = data.PageSize ?? options.DefaultPageSize;
            var pageNumber = data.PageNumber ?? options.DefaultPageNumber;
            var endpoint = string.Format(options.SearchEndpointTemplate!, data.Query, pageSize, pageSize * (pageNumber - 1) + 1);

            //TODO: Add retry policy using Polly: https://www.pollydocs.org/getting-started.html
            var response = await http.GetAsync(endpoint);
            var responseResolver = serviceResolver
                .Resolve<IGoogleSearchResponseResolver>(response.StatusCode);

            var result = await responseResolver!.Resolve(response);
            return result;
        }
    }
}
