
namespace MaccabiSearch.Application.Models
{
    /// <summary>
    /// Error Response Data Transfer Object
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Response data.
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Request metadata (<seealso cref="IRequestMetadata"/>).
        /// </summary>
        public IRequestMetadata? RequestMetadata { get; set; }

        /// <summary>
        /// Error code value.
        /// </summary>
        public string Error { get; set; } = ErrorCode.UnexpectedExceptionOccured;
    }
}
