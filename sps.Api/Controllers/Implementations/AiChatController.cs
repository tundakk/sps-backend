//// ChatController.cs
//using sps.Api.Controllers.Implementations;
//using sps.BLL.Infrastructure.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.SemanticKernel;
//using Microsoft.SemanticKernel.ChatCompletion;

///// <summary>
///// Controller for handling chat-related requests.
///// </summary>
//[ApiController]
//[Route("api/[controller]")]
//public class AiChatController : ControllerBase
//{
//    private readonly IChatService chatService;
//    private readonly Kernel kernel;

//    private ChatHistory chatHistory;
//    private KernelArguments arguments;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="ChatController"/> class.
//    /// </summary>
//    /// <param name="kernel">The kernel instance to handle chat requests.</param>
//    /// <param name="chatService">Service to handle chat messages.</param>
//    public AiChatController(Kernel kernel, IChatService chatService)
//    {
//        this.kernel = kernel;
//        this.chatService = chatService;
//        this.chatHistory = new ChatHistory();
//        this.arguments = new KernelArguments { { "chatHistory", chatHistory } };
//    }

//    /// <summary>
//    /// Handles chat requests by invoking the kernel with the provided prompt.
//    /// </summary>
//    /// <param name="request">The chat request containing the prompt.</param>
//    /// <returns>The result of the chat request.</returns>
//    [HttpPost("ask")]
//    public async Task<IActionResult> Ask([FromBody] ChatRequest request)
//    {
//        // Insert the incoming message.
//        var userMessage = new sps.Domain.Model.Entities.ChatMessage
//        {
//            Sender = "User",
//            Message = request.Prompt
//        };
//        await chatService.InsertAsync(userMessage);

//        // Create context variables and set the prompt
//        var contextVariables = new ContextVariables();
//        contextVariables.Set("input", request.Prompt);

//        // Invoke the semantic function
//        var result = await kernel.RunAsync(contextVariables, chatFunction);

//        // Insert system response.
//        var botMessage = new sps.Domain.Model.Entities.ChatMessage
//        {
//            Sender = "Bot",
//            Message = result.Result
//        };
//        await chatService.InsertAsync(botMessage);

//        return Ok(result.Result);
//    }
//}

///// <summary>
///// Represents a chat request.
///// </summary>
//public class ChatRequest
//{
//    /// <summary>
//    /// Gets or sets the prompt for the chat request.
//    /// </summary>
//    public required string Prompt { get; set; }
//}