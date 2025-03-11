using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using System;
using System.Threading.Tasks;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing comments.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : BaseController<CommentsController>
    {
        private readonly ICommentService _commentService;

        /// <summary>
        /// Constructor for the CommentsController.
        /// </summary>
        /// <param name="commentService">The comment service</param>
        /// <param name="logger">The logger</param>
        public CommentsController(ICommentService commentService, ILogger<CommentsController> logger)
            : base(logger)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Adds a comment to an entity
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddCommentAsync([FromBody] CommentModel comment)
        {
            try
            {
                var result = await _commentService.AddCommentAsync(comment);
                return ProcessResponse(result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets comments for a specific entity
        /// </summary>
        [HttpGet("{entityType}/{entityId}")]
        public async Task<IActionResult> GetCommentsByEntityAsync(string entityType, Guid entityId)
        {
            try
            {
                var result = await _commentService.GetCommentsByEntityAsync(entityType, entityId);
                return ProcessResponse(result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes a comment
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentAsync(Guid id)
        {
            try
            {
                var result = await _commentService.DeleteCommentAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}