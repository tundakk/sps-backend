namespace sps.API.Middleware
{
    /// <summary>
    /// Extension methods for registering custom middleware
    /// </summary>
    public static class MiddlewareExtensions
    {
        ///// <summary>
        ///// Adds the JWT middleware to the application pipeline
        ///// </summary>
        //public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        //{
        //    return builder.UseMiddleware<JwtMiddleware>();
        //}

        /// <summary>
        /// Adds the global exception handler middleware to the application pipeline
        /// </summary>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}