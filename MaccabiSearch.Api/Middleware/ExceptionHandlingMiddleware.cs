using MaccabiSearch.Application.Models;

namespace MaccabiSearch.Api.Middleware
{
    /// <summary>
    /// Middleware handling exceptions if occured.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger
        )
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// A function that can process an HTTP request.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> for the request.</param>
        /// <param name="requestMetadata">The <see cref="IRequestMetadata"/> of the request.</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, IRequestMetadata requestMetadata)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var response = new ErrorResponse
                {
                    RequestMetadata = requestMetadata,
                };

                logger.LogCritical(exception, "{0} - {1}: {2}",
                    response.RequestMetadata.TrackingId, response.Error, exception.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
