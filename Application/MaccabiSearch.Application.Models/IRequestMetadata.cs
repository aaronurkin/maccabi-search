namespace MaccabiSearch.Application.Models
{
    /// <summary>
    /// Contract of request metadata object.
    /// </summary>
    public interface IRequestMetadata
    {
        /// <summary>
        /// An identifier provided by a client to track a process consisting of one or more requests and actions.
        /// </summary>
        string? TrackingId { get; }
    }
}