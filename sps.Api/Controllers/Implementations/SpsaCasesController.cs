using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// SpsaCasesController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SpsaCasesController : BaseController<SpsaCasesController>
    {
        private readonly ISpsaCaseService _spsaCaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpsaCasesController"/> class.
        /// </summary>
        /// <param name="spsaCaseService">The SpsaCase service.</param>
        /// <param name="logger">The logger.</param>
        public SpsaCasesController(ISpsaCaseService spsaCaseService, ILogger<SpsaCasesController> logger)
            : base(logger)
        {
            _spsaCaseService = spsaCaseService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _spsaCaseService.GetAllAsync();
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
                var response = await _spsaCaseService.GetByIdAsync(id);
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
        public async Task<IActionResult> InsertAsync([FromBody] SpsaCaseModel spsaCaseModel)
        {
            try
            {
                var response = await _spsaCaseService.InsertAsync(spsaCaseModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the SpsaCase");
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SpsaCaseModel spsaCaseModel)
        {
            try
            {
                spsaCaseModel.Id = id;
                var response = await _spsaCaseService.UpdateAsync(spsaCaseModel);
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
                var response = await _spsaCaseService.DeleteAsync(id);
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