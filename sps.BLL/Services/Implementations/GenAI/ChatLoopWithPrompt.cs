//namespace sps.BLL.Services.Implementations.GenAI
//{
//    using sps.BLL.Services.Interfaces.GenAI;
//    using Microsoft.SemanticKernel;
//    using Microsoft.SemanticKernel.ChatCompletion;
//    using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
//    using System.Threading.Tasks;

//    /// <summary>
//    /// This class demonstrates how to render a chat history to a
//    /// prompt and use chat completion prompts in a loop.
//    /// </summary>
//    public sealed class ChatLoopWithPrompt : IChatLoopWithPrompt
//    {
//        private readonly Kernel kernel;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="ChatLoopWithPrompt"/> class.
//        /// </summary>
//        /// <param name="kernel">The kernel instance to be used for chat completion.</param>
//        public ChatLoopWithPrompt(Kernel kernel)
//        {
//            this.kernel = kernel;
//        }

//        /// <summary>
//        /// Executes the chat loop as a prompt asynchronously.
//        /// </summary>
//        /// <returns>A task that represents the asynchronous operation.</returns>
//        public async Task ExecuteChatLoopAsPromptAsync()
//        {
//            var chatHistory = new ChatHistory();
//            KernelArguments arguments = new() { { "chatHistory", chatHistory } };

//            string[] userMessages = [
//                "What is Seattle?",
//                     "What is the population of Seattle?",
//                     "What is the area of Seattle?",
//                     "What is the weather in Seattle?",
//                     "What is the zip code of Seattle?",
//                     "What is the elevation of Seattle?",
//                     "What is the latitude of Seattle?",
//                     "What is the longitude of Seattle?",
//                     "What is the mayor of Seattle?"
//            ];

//            foreach (var userMessage in userMessages)
//            {
//                chatHistory.AddUserMessage(userMessage);
//                OutputLastMessage(chatHistory);

//                var function = kernel.CreateFunctionFromPrompt(
//                    new()
//                    {
//                        Template =
//                        """
//                             {{#each (chatHistory)}}
//                             <message role="{{Role}}">{{Content}}</message>
//                             {{/each}}
//                             """,
//                        TemplateFormat = "handlebars"
//                    },
//                    new HandlebarsPromptTemplateFactory()
//                );

//                var response = await kernel.InvokeAsync(function, arguments);

//                chatHistory.AddAssistantMessage(response.ToString());
//                OutputLastMessage(chatHistory);
//            }
//        }
//    }
//}