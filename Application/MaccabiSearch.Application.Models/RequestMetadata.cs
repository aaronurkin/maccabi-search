namespace MaccabiSearch.Application.Models
{
    /// <summary>
    /// Encapsulates metadata of a particular request.
    /// </summary>
    public class RequestMetadata : IRequestMetadata
    {
        public RequestMetadata(string? trackingId)
        {
            TrackingId = trackingId;
        }

        /// <summary>
        /// An identifier provided by a client to track a process consisting of one or more requests and actions.
        /// </summary>
        public string? TrackingId { get; }
    }
}
