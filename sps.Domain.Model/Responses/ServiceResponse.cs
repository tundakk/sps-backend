namespace sps.Domain.Model.Responses
{
    /// <summary>
    /// This class wraps the object in a property called Data. it has Success and Message as other properties, that is used to send back if the Data property has data in it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Placeholder type intended to be populated by a object or list of objects.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Set to false if property 'Data' has no data.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Error message set when Success is false, to give context of the problem.
        /// </summary>
        public string Message { get; set; } = string.Empty;
        
        /// <summary>
        /// Optional error code for more structured error handling by clients.
        /// </summary>
        public string ErrorCode { get; set; } = string.Empty;
        
        /// <summary>
        /// Collection of field-specific validation errors.
        /// </summary>
        public Dictionary<string, List<string>>? ValidationErrors { get; set; }
        
        /// <summary>
        /// Technical details of the error (typically only shown in development environments).
        /// </summary>
        public string? TechnicalDetails { get; set; }

        /// <summary>
        /// Creates a success response with the specified data.
        /// </summary>
        /// <param name="data">The data to include in the response.</param>
        /// <returns>A success ServiceResponse with the specified data.</returns>
        public static ServiceResponse<T> CreateSuccess(T data)
        {
            return new ServiceResponse<T>
            {
                Success = true,
                Data = data
            };
        }

        /// <summary>
        /// Creates an error response with the specified message.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="errorCode">Optional error code.</param>
        /// <returns>An error ServiceResponse with the specified message.</returns>
        public static ServiceResponse<T> CreateError(string message, string errorCode = "")
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode
            };
        }

        /// <summary>
        /// Creates a validation error response.
        /// </summary>
        /// <param name="validationErrors">Dictionary of validation errors by field.</param>
        /// <param name="message">Optional general error message.</param>
        /// <returns>A validation error ServiceResponse.</returns>
        public static ServiceResponse<T> CreateValidationError(
            Dictionary<string, List<string>> validationErrors,
            string message = "Validation failed")
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                ErrorCode = "VALIDATION_ERROR",
                ValidationErrors = validationErrors
            };
        }

        /// <summary>
        /// Creates a "not found" error response.
        /// </summary>
        /// <param name="message">Optional error message.</param>
        /// <returns>A "not found" error ServiceResponse.</returns>
        public static ServiceResponse<T> CreateNotFound(string message = "Resource not found")
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                ErrorCode = "NOT_FOUND"
            };
        }
    }
}