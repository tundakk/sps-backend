//using sps.BLL.Infrastructure.Interfaces;
//using sps.Domain.Model.Entities;
//using sps.Domain.Model.Responses;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.SemanticKernel;
//using Microsoft.SemanticKernel.ChatCompletion;
//using OpenAI.Chat;

//// TODO: make a domain model without id
//namespace sps.Api.Controllers.Implementations
//{
//    /// <summary>
//    /// Controller for handling chat-related operations.
//    /// </summary>
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ChatController : ControllerBase
//    {
//        private readonly IChatService chatService;
//        private readonly Kernel kernel;

//        private ChatHistory chatHistory;
//        private KernelArguments arguments;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="ChatController"/> class.
//        /// </summary>
//        /// <param name="chatService">The chat service.</param>
//        public ChatController(IChatService chatService)
//        {
//            this.chatService = chatService;
//            this.kernel = kernel;
//            this.chatService = chatService;
//            this.chatHistory = new ChatHistory();
//            this.arguments = new KernelArguments { { "chatHistory", chatHistory } };
//        }

//        /// <summary>
//        /// Gets all chat messages.
//        /// </summary>
//        /// <returns>A list of chat messages.</returns>
//        [HttpGet]
//        public async Task<ActionResult<ServiceResponse<IEnumerable<ChatMessage>>>> GetAllAsync()
//        {
//            var response = await chatService.GetAllAsync();
//            if (!response.Success)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }

//        /// <summary>
//        /// Handles chat requests by invoking the kernel with the provided prompt.
//        /// </summary>
//        /// <param name="request">The chat request containing the prompt.</param>
//        /// <returns>The result of the chat request.</returns>
//        [HttpPost("ask")]
//        public async Task<IActionResult> Ask([FromBody] ChatRequest request)
//        {
//            // Insert the incoming message.
//            var userMessage = new sps.Domain.Model.Entities.ChatMessage
//            {
//                Sender = "User",
//                Message = request.Prompt
//            };
//            await chatService.InsertAsync(userMessage);

//            // Create context variables and set the prompt
//            var contextVariables = new ContextVariables();
//            contextVariables.Set("input", request.Prompt);

//            // Invoke the semantic function
//            var result = await kernel.RunAsync(contextVariables, chatFunction);

//            // Insert system response.
//            var botMessage = new sps.Domain.Model.Entities.ChatMessage
//            {
//                Sender = "Bot",
//                Message = result.Result
//            };
//            await chatService.InsertAsync(botMessage);

//            return Ok(result.Result);
//        }
//}

//        /// <summary>
//        /// Inserts a new chat message.
//        /// </summary>
//        /// <param name="message">The chat message to insert.</param>
//        /// <returns>The inserted chat message.</returns>
//        [HttpPost]
//        public async Task<ActionResult<ServiceResponse<ChatMessage>>> InsertAsync([FromBody] ChatMessage message)
//        {
//            var response = await _chatService.InsertAsync(message);
//            if (!response.Success)
//            {
//                return BadRequest(response);
//            }
//            return Ok(response);
//        }
//    }
//}