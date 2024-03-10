namespace MaccabiSearch.Common.Models
{
    /// <summary>
    /// Service Result Status possible values
    /// </summary>
    public enum ServiceResultStatus
    {
        /// <summary>
        /// Default value to prevent a wrong value.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Operation failed.
        /// </summary>
        Failed = 1,

        /// <summary>
        /// Operation succeeded.
        /// </summary>
        Succeeded = 2,

        /// <summary>
        /// An invalid data has been passed.
        /// </summary>
        InvalidInput = 3,

        /// <summary>
        /// The resource alerady exists (duplication).
        /// </summary>
        AlreadyExisting = 4,

        /// <summary>
        /// The requested resource was not found.
        /// </summary>
        NotFound = 5,

        /// <summary>
        /// The request isn't allowed to fulfill.
        /// </summary>
        Forbidden = 6
    }
}
