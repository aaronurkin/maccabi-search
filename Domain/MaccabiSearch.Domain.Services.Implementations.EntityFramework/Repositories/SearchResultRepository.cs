using MaccabiSearch.Domain.Models;

namespace MaccabiSearch.Domain.Services.Implementations
{
    /// <summary>
    /// Search Results Repository
    /// </summary>
    public class SearchResultRepository : RepositoryBase<SearchResultPgEntity>
    {
        public SearchResultRepository(MaccabiSearchDbContext context)
            : base(context) { }
    }
}
