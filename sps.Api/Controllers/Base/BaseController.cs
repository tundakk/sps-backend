using Microsoft.AspNetCore.Mvc;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Base
{
    /// <summary>
    /// Base controller for API controllers.
    /// </summary>
    /// <typeparam name="T">The type of the controller.</typeparam>
    [ApiController]
    public abstract class BaseController<T> : ControllerBase
    {
        /// <summary>
        /// The logger instance used for logging.
        /// </summary>
        protected readonly ILogger<T> Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController{T}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected BaseController(ILogger<T> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Handles the error and returns an error response.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>The error response.</returns>
        protected IActionResult HandleError(Exception ex)
        {
            Logger.LogError(ex, ex.Message);

            var response = ServiceResponse<object>.CreateError(
                "An unexpected error occurred. Please try again later.",
                "INTERNAL_SERVER_ERROR");

            // Add technical details in development environment
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                response.TechnicalDetails = $"{ex.GetType().Name}: {ex.Message}\n{ex.StackTrace}";
            }

            return StatusCode(500, response);
        }

        /// <summary>
        /// Processes and returns the appropriate result based on the service response.
        /// </summary>
        /// <typeparam name="TData">The type of data in the response.</typeparam>
        /// <param name="response">The service response to process.</param>
        /// <returns>The appropriate IActionResult based on the service response.</returns>
        protected IActionResult ProcessResponse<TData>(ServiceResponse<TData> response)
        {
            return this.ToActionResult(response);
        }

        /// <summary>
        /// Creates a result for newly created resources.
        /// </summary>
        /// <typeparam name="TData">The type of data in the response.</typeparam>
        /// <param name="response">The service response to process.</param>
        /// <param name="actionName">The action name for the location header.</param>
        /// <param name="routeValues">The route values for the location header.</param>
        /// <returns>The appropriate IActionResult for a newly created resource.</returns>
        protected IActionResult CreatedResponse<TData>(
            ServiceResponse<TData> response,
            string actionName,
            object routeValues)
        {
            return this.ToCreatedResult(response, actionName, routeValues);
        }
    }
}