using MaccabiSearch.Common.Models;
using MaccabiSearch.Common.Services;
using MaccabiSearch.Domain.Models;
using MaccabiSearch.Domain.Services;
using MaccabiSearch.Infrastructure.Models;
using MaccabiSearch.Infrastructure.Services;

namespace MaccabiSearch.Application.Services.Implementations
{
    /// <summary>
    /// The application service running search queries against all search engines.
    /// </summary>
    public class SearchResultsManager : AllEnginesSearchService
    {
        private readonly ICacheService cacheService;
        private readonly IRepository<SearchResultPgEntity> repository;
        private readonly IModelMapper<SearchResult, SearchResultPgEntity> entityMapper;

        public SearchResultsManager(
            ICacheService cacheService,
            IEnumerable<ISearchEngineApiClient> clients,
            IRepository<SearchResultPgEntity> repository,
            IModelMapper<SearchResult, SearchResultPgEntity> entityMapper
        )
            : base(clients)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.entityMapper = entityMapper ?? throw new ArgumentNullException(nameof(entityMapper));
            this.cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        /// <summary>
        /// Searches in all engines.
        /// </summary>
        /// <param name="data">Search data.</param>
        /// <returns>Search results wrapped with the <see cref="IServiceResult"/>.</returns>
        public override async Task<IServiceResult> Search(SearchDto data)
        {
            var cacheKey = $"search-queries-{data.Query}";
            var existingResults = await cacheService.Get<IEnumerable<SearchResult>>(cacheKey);

            if (existingResults != null)
            {
                return new ServiceResult<IEnumerable<SearchResult>>(existingResults, ServiceResultStatus.Succeeded);
            }

            var result = await base.Search(data);

            if (result.Status != ServiceResultStatus.Succeeded)
            {
                return result;
            }

            var searchResults = result.Data as IEnumerable<SearchResult>;
            var entities = searchResults!.Select(entityMapper.Map);

            await Task.WhenAll(cacheService.Set(cacheKey, searchResults), repository.Create(entities));

            return result;
        }
    }
}
