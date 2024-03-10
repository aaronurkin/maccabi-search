namespace MaccabiSearch.Infrastructure.Models
{
    /// <summary>
    /// Search parameters contract
    /// </summary>
    public interface ISearchDto
    {
        /// <summary>
        /// Gets items batch number 
        /// </summary>
        int? PageNumber { get; }

        /// <summary>
        /// Gets items batch number
        /// </summary>
        int? PageSize { get; }

        /// <summary>
        /// Gets the search query
        /// </summary>
        string? Query { get; }
    }
}