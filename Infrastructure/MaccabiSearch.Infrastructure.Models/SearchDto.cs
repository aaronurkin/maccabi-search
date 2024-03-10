namespace MaccabiSearch.Infrastructure.Models
{
    /// <summary>
    /// Search parameters
    /// </summary>
    public class SearchDto : ISearchDto
    {
        /// <summary>
        /// Gets or sets items batch number
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Gets or sets items batch number
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the search query
        /// </summary>
        public string? Query { get; set; }
    }
}
