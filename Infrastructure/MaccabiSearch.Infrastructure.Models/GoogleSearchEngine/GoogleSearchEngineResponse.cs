namespace MaccabiSearch.Infrastructure.Models.GoogleSearchEngine
{
    /// <summary>
    /// The response from the Google Search Engine API
    /// </summary>
    public class GoogleSearchEngineResponse : IGoogleSearchEngineResponse
    {
        /// <summary>
        /// Gets or sets found items
        /// </summary>
        public IEnumerable<GoogleSearchEngineResponseItem>? Items { get; set; }
    }
}
