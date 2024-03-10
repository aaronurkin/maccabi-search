namespace MaccabiSearch.Common.Models
{
    public class ServiceResult<TData> : IServiceResult
    {
        public ServiceResult(ServiceResultStatus status)
            : this(default, status) { }

        public ServiceResult(TData? data, ServiceResultStatus status)
        {
            Data = data;
            Status = status;
        }

        /// <summary>
        /// Gets result data
        /// </summary>
        public object? Data { get; }

        /// <summary>
        /// Gets result status
        /// </summary>
        public ServiceResultStatus Status { get; }
    }
}
