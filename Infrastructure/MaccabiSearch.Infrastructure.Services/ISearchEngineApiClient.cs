using MaccabiSearch.Common.Models;
using MaccabiSearch.Infrastructure.Models;

namespace MaccabiSearch.Infrastructure.Services
{
    public interface ISearchEngineApiClient
    {
        Task<IServiceResult> Search(SearchDto data);
    }
}
