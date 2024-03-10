namespace MaccabiSearch.Infrastructure.Models
{
    /// <summary>
    /// Common options for the Search Engine API Client
    /// </summary>
    public interface ISearchEngineApiClientOptions
    {
        /// <summary>
        /// Gets the Search Engine API key.
        /// </summary>
        string? ApiKey { get; }

        /// <summary>
        /// Gets the default items count per request.
        /// </summary>
        int? DefaultPageSize { get; }

        /// <summary>
        /// Gets the default page number.
        /// </summary>
        int? DefaultPageNumber { get; }

        /// <summary>
        /// Gets the Search Engine API search endpoint template.
        /// </summary>
        string? SearchEndpointTemplate { get; }
    }
}