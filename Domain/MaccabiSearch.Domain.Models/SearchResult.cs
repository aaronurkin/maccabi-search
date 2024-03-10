namespace MaccabiSearch.Domain.Models
{
    /// <summary>
    /// Represents Search Result domain entity.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Gets or sets the result title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the result entered date.
        /// </summary>
        public DateTime? EnteredDate { get; set; }

        /// <summary>
        /// Gets or sets the Search Engine the result received from.
        /// </summary>
        public SearchEngine? SearchEngine { get; set; }
    }
}
