
namespace MaccabiSearch.Infrastructure.Models.GoogleSearchEngine
{
    /// <summary>
    /// Contract of the response from the Google Search Engine API
    /// </summary>
    public interface IGoogleSearchEngineResponse
    {
        /// <summary>
        /// Gets found items
        /// </summary>
        IEnumerable<GoogleSearchEngineResponseItem>? Items { get; }
    }
}