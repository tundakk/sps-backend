using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// EducationPeriodRatesController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EducationPeriodRatesController : BaseController<EducationPeriodRatesController>
    {
        private readonly IEducationPeriodRateService _educationPeriodRateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EducationPeriodRatesController"/> class.
        /// </summary>
        /// <param name="educationPeriodRateService">The EducationPeriodRate service.</param>
        /// <param name="logger">The logger.</param>
        public EducationPeriodRatesController(IEducationPeriodRateService educationPeriodRateService, ILogger<EducationPeriodRatesController> logger)
            : base(logger)
        {
            _educationPeriodRateService = educationPeriodRateService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _educationPeriodRateService.GetAllAsync();
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
                var response = await _educationPeriodRateService.GetByIdAsync(id);
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
        public async Task<IActionResult> InsertAsync([FromBody] EducationPeriodRateModel educationPeriodRateModel)
        {
            try
            {
                var response = await _educationPeriodRateService.InsertAsync(educationPeriodRateModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the EducationPeriodRate");
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EducationPeriodRateModel educationPeriodRateModel)
        {
            try
            {
                educationPeriodRateModel.Id = id;
                var response = await _educationPeriodRateService.UpdateAsync(educationPeriodRateModel);
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
                var response = await _educationPeriodRateService.DeleteAsync(id);
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