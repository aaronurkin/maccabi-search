using MaccabiSearch.Common.Models;
using MaccabiSearch.Domain.Models;
using MaccabiSearch.Infrastructure.Models;
using MaccabiSearch.Infrastructure.Services;

namespace MaccabiSearch.Application.Services.Implementations
{
    /// <summary>
    /// The application service running search queries all together.
    /// </summary>
    public class AllEnginesSearchService : ISearchService
    {
        private readonly IEnumerable<ISearchEngineApiClient> clients;

        public AllEnginesSearchService(IEnumerable<ISearchEngineApiClient> clients)
        {
            this.clients = clients ?? throw new ArgumentNullException(nameof(clients));
        }

        /// <summary>
        /// Searches in all engines.
        /// </summary>
        /// <param name="data">Search data.</param>
        /// <returns>Search results wrapped with the <see cref="IServiceResult"/>.</returns>
        public async Task<IServiceResult> Search(SearchDto data)
        {
            var clientResults = await Task.WhenAll(clients.Select(client => client.Search(data)));
            var results = clientResults
                .Where(r => r.Status == ServiceResultStatus.Succeeded)
                .Select(r => r.Data as IEnumerable<SearchResult>);

            if (!results.Any())
            {
                var resultData = clientResults.Where(r => r.Status != ServiceResultStatus.Succeeded);
                return new ServiceResult<IEnumerable<IServiceResult>>(resultData, ServiceResultStatus.Failed);
            }

            //TODO: Save results to the DB using one of options:
            //  - Use a repository depending on EntityFramework
            //  - Send a message to queue to save results async to respond faster to a client.
            var result = new ServiceResult<IEnumerable<IEnumerable<SearchResult>>>(results!, ServiceResultStatus.Succeeded);
            return result;
        }
    }
}
