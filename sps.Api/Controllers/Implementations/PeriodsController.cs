using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// PeriodsController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodsController : BaseController<PeriodsController>
    {
        private readonly IPeriodService _periodService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodsController"/> class.
        /// </summary>
        /// <param name="periodService">The Period service.</param>
        /// <param name="logger">The logger.</param>
        public PeriodsController(IPeriodService periodService, ILogger<PeriodsController> logger)
            : base(logger)
        {
            _periodService = periodService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _periodService.GetAllAsync();
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
                var response = await _periodService.GetByIdAsync(id);
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
        public async Task<IActionResult> InsertAsync([FromBody] PeriodModel periodModel)
        {
            try
            {
                var response = await _periodService.InsertAsync(periodModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the Period");
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] PeriodModel periodModel)
        {
            try
            {
                periodModel.Id = id;
                var response = await _periodService.UpdateAsync(periodModel);
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
                var response = await _periodService.DeleteAsync(id);
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