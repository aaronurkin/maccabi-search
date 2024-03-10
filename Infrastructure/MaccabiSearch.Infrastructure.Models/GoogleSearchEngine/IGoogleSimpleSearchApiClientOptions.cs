namespace MaccabiSearch.Infrastructure.Models
{
    /// <summary>
    /// Contract of the Google Search Engine API response.
    /// </summary>
    public interface IGoogleSimpleSearchApiClientOptions : ISearchEngineApiClientOptions
    {
        /// <summary>
        /// The identitier of the engine.
        /// </summary>
        string? SearchEngineId { get; set; }
    }
}