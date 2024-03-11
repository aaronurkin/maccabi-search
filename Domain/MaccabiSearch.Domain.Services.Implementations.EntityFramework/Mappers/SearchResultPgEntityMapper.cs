using MaccabiSearch.Common.Services;
using MaccabiSearch.Domain.Models;

namespace MaccabiSearch.Domain.Services.Implementations.EntityFramework
{
    public class SearchResultPgEntityMapper : IModelMapper<SearchResult, SearchResultPgEntity>
    {
        public SearchResultPgEntity Map(SearchResult source)
        {
            var target = Map(source, new SearchResultPgEntity());
            return target;
        }

        public SearchResultPgEntity Map(SearchResult source, SearchResultPgEntity target)
        {
            target.Title = source.Title;
            target.EnteredDate = source.EnteredDate;
            target.SearchEngine = source.SearchEngine;

            return target;
        }
    }
}
