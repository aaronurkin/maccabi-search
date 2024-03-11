namespace MaccabiSearch.Domain.Models
{
    /// <summary>
    /// Represents a search result database entry.
    /// </summary>
    public class SearchResultPgEntity : SearchResult
    {
        /// <summary>
        /// Search result identifier.
        /// </summary>
        public string Id { get; set; }
    }
}
