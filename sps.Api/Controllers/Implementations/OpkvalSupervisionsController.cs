using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// OpkvalSupervisionsController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OpkvalSupervisionsController : BaseController<OpkvalSupervisionsController>
    {
        private readonly IOpkvalSupervisionService _opkvalSupervisionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpkvalSupervisionsController"/> class.
        /// </summary>
        /// <param name="opkvalSupervisionService">The OpkvalSupervision service.</param>
        /// <param name="logger">The logger.</param>
        public OpkvalSupervisionsController(IOpkvalSupervisionService opkvalSupervisionService, ILogger<OpkvalSupervisionsController> logger)
            : base(logger)
        {
            _opkvalSupervisionService = opkvalSupervisionService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _opkvalSupervisionService.GetAllAsync();
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        ///<inheritdoc/>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _opkvalSupervisionService.GetByIdAsync(id);
                if (!response.Success)
                {
                    return NotFound(response.Message); // Use NotFound for invalid IDs
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        ///<inheritdoc/>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] OpkvalSupervisionModel opkvalSupervisionModel)
        {
            try
            {
                var response = await _opkvalSupervisionService.InsertAsync(opkvalSupervisionModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the OpkvalSupervision");
                }
                return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Data.Id }, response.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        ///<inheritdoc/>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] OpkvalSupervisionModel opkvalSupervisionModel)
        {
            try
            {
                opkvalSupervisionModel.Id = id;
                var response = await _opkvalSupervisionService.UpdateAsync(opkvalSupervisionModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        ///<inheritdoc/>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _opkvalSupervisionService.DeleteAsync(id);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Message); // Return a success message or confirmation
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}