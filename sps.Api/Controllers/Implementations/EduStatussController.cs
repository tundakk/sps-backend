using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// EduStatussController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EduStatussController : BaseController<EduStatussController>
    {
        private readonly IEduStatusService _eduStatusService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EduStatussController"/> class.
        /// </summary>
        /// <param name="eduStatusService">The EduStatus service.</param>
        /// <param name="logger">The logger.</param>
        public EduStatussController(IEduStatusService eduStatusService, ILogger<EduStatussController> logger)
            : base(logger)
        {
            _eduStatusService = eduStatusService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _eduStatusService.GetAllAsync();
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
                var response = await _eduStatusService.GetByIdAsync(id);
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
        public async Task<IActionResult> InsertAsync([FromBody] EduStatusModel eduStatusModel)
        {
            try
            {
                var response = await _eduStatusService.InsertAsync(eduStatusModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the EduStatus");
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EduStatusModel eduStatusModel)
        {
            try
            {
                eduStatusModel.Id = id;
                var response = await _eduStatusService.UpdateAsync(eduStatusModel);
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
                var response = await _eduStatusService.DeleteAsync(id);
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