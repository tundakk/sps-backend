//// SemanticKernelExtensions.cs

//namespace sps.BLL
//{
//    using sps.BLL.Plugins;
//    using Microsoft.Extensions.DependencyInjection;
//    using Microsoft.SemanticKernel;
//    using Microsoft.SemanticKernel.ChatCompletion;
//    using Microsoft.SemanticKernel.Connectors.OpenAI;

//    /// <summary>
//    /// Extension methods for configuring Semantic Kernel services.
//    /// </summary>
//    public static class SemanticKernelExtensions
//    {
//        /// <summary>
//        /// Adds and configures Semantic Kernel services.
//        /// </summary>
//        /// <param name="services">The service collection to add the Semantic Kernel to.</param>
//        /// <returns>The updated service collection.</returns>
//        public static IServiceCollection AddSemanticKernel(this IServiceCollection services)
//        {
//            // Create a kernel builder
//            var builder = Kernel.CreateBuilder();

//            // Configure the kernel with necessary services, e.g., OpenAI
//            builder.AddOpenAIChatCompletion(
//                modelId: "gtp-4o",
//                apiKey: "
//            );

//            // Register the LightsPlugin
//            builder.Plugins.AddFromType<LightsPlugin>("Lights");
//            builder.Plugins.AddFromType<BookingsPlugin>("Bookings");

//            // Build the kernel and add it to the service collection
//            Kernel kernel = builder.Build();
//            var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
//            services.AddSingleton(kernel);

//            // Enable planning
//            OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
//            {
//                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
//            };

//            return services;
//        }
//    }
//}