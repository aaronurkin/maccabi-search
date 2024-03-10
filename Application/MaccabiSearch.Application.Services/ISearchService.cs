using MaccabiSearch.Common.Models;
using MaccabiSearch.Infrastructure.Models;

namespace MaccabiSearch.Application.Services
{
    /// <summary>
    /// Contract of the application service running search queries.
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Searches in all engines.
        /// </summary>
        /// <param name="data">Search data.</param>
        /// <returns>Search results wrapped with the <see cref="IServiceResult"/>.</returns>
        Task<IServiceResult> Search(SearchDto data);
    }
}
