using Microsoft.AspNetCore.Mvc;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Base
{
    /// <summary>
    /// Extension methods for handling ServiceResponse objects in controllers.
    /// </summary>
    public static class ServiceResponseExtensions
    {
        /// <summary>
        /// Converts a ServiceResponse to an appropriate IActionResult.
        /// </summary>
        /// <typeparam name="T">The type of data in the ServiceResponse.</typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="response">The service response.</param>
        /// <returns>An appropriate IActionResult based on the ServiceResponse.</returns>
        public static IActionResult ToActionResult<T>(this ControllerBase controller, ServiceResponse<T> response)
        {
            if (response.Success)
            {
                if (response.Data == null)
                {
                    return controller.NoContent();
                }
                return controller.Ok(response);
            }
            
            // Check for common error scenarios
            if (response.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return controller.NotFound(response);
            }
            
            if (response.Message.Contains("unauthorized", StringComparison.OrdinalIgnoreCase) ||
                response.Message.Contains("not authorized", StringComparison.OrdinalIgnoreCase))
            {
                return controller.Unauthorized(response);
            }
            
            if (response.Message.Contains("validation", StringComparison.OrdinalIgnoreCase) ||
                response.Message.Contains("invalid", StringComparison.OrdinalIgnoreCase))
            {
                return controller.BadRequest(response);
            }
            
            // Default to BadRequest for failures
            return controller.BadRequest(response);
        }
        
        /// <summary>
        /// Creates a created-at-action result with the response.
        /// </summary>
        /// <typeparam name="T">The type of data in the ServiceResponse.</typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="response">The service response.</param>
        /// <param name="actionName">Name of the action for the Created route.</param>
        /// <param name="routeValues">Route values for the Created route.</param>
        /// <returns>An appropriate IActionResult based on the ServiceResponse.</returns>
        public static IActionResult ToCreatedResult<T>(this ControllerBase controller, 
            ServiceResponse<T> response, 
            string actionName, 
            object routeValues)
        {
            if (response.Success && response.Data != null)
            {
                return controller.CreatedAtAction(actionName, routeValues, response);
            }
            
            return controller.ToActionResult(response);
        }
    }
}