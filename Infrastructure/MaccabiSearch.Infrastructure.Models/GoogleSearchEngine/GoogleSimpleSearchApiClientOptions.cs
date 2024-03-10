namespace MaccabiSearch.Infrastructure.Models
{
    /// <summary>
    /// The Google Search Engine API response.
    /// </summary>
    public class GoogleSimpleSearchApiClientOptions : SearchEngineApiClientOptions, IGoogleSimpleSearchApiClientOptions
    {
        /// <summary>
        /// The identitier of the engine.
        /// </summary>
        public string? SearchEngineId { get; set; }
    }
}
