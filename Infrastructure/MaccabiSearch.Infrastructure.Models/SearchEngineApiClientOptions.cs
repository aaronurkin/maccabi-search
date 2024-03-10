namespace MaccabiSearch.Infrastructure.Models
{
    /// <summary>
    /// Common options for the Search Engine API Client
    /// </summary>
    public class SearchEngineApiClientOptions : ISearchEngineApiClientOptions
    {
        /// <summary>
        /// Gets or sets the Search Engine API key.
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the default items count per request.
        /// </summary>
        public int? DefaultPageSize { get; set; }

        /// <summary>
        /// Gets or sets the default page number.
        /// </summary>
        public int? DefaultPageNumber { get; set; }

        /// <summary>
        /// Gets or sets the Search Engine API search endpoint template.
        /// </summary>
        public string? SearchEndpointTemplate { get; set; }
    }
}
