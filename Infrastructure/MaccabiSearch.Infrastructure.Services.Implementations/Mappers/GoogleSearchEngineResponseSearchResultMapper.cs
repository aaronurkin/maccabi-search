using MaccabiSearch.Common.Services;
using MaccabiSearch.Domain.Models;
using MaccabiSearch.Infrastructure.Models.GoogleSearchEngine;

namespace MaccabiSearch.Infrastructure.Services.Implementations.Mappers
{
    /// <summary>
    /// The mapper from the <see cref="GoogleSearchEngineResponse"/> into the <see cref="IEnumerable{SearchResult}"/>
    /// </summary>
    public class GoogleSearchEngineResponseSearchResultMapper : IModelMapper<GoogleSearchEngineResponse, IEnumerable<SearchResult>>
    {
        /// <summary>
        /// Initializes the target instance and maps the data to it from the source.
        /// </summary>
        /// <param name="source">The instance to map data from.</param>
        /// <returns>The mapped instance of the <see cref="IEnumerable{SearchResult}"/> type.</returns>
        public IEnumerable<SearchResult> Map(GoogleSearchEngineResponse source)
        {
            var target = Map(source, new List<SearchResult>());
            return target;
        }

        /// <summary>
        /// Maps the data from the source instance into the target.
        /// </summary>
        /// <param name="source">The instance to map data from.</param>
        /// <param name="target">The instance to map data to.</param>
        /// <returns>The mapped instance of the <see cref="IEnumerable{SearchResult}"/> type.</returns>
        public IEnumerable<SearchResult> Map(GoogleSearchEngineResponse source, IEnumerable<SearchResult> target)
        {
            target = source.Items!
                .Select(item => new SearchResult
                {
                    Title = item.Title,
                    EnteredDate = DateTime.UtcNow,
                    SearchEngine = SearchEngine.Google,
                });

            return target;
        }
    }
}
