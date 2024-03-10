namespace MaccabiSearch.Common.Models
{
    /// <summary>
    /// Service Result Contract
    /// </summary>
    public interface IServiceResult
    {
        /// <summary>
        /// Gets result data
        /// </summary>
        object? Data { get; }

        /// <summary>
        /// Gets result status
        /// </summary>
        ServiceResultStatus Status { get; }
    }
}
