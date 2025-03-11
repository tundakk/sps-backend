using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using Mapster;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing academic periods
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodsController : BaseController<PeriodsController>
    {
        private readonly IPeriodService _periodService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodsController"/> class.
        /// </summary>
        /// <param name="periodService">The period service.</param>
        /// <param name="logger">The logger.</param>
        public PeriodsController(IPeriodService periodService, ILogger<PeriodsController> logger)
            : base(logger)
        {
            _periodService = periodService;
        }

        /// <summary>
        /// Gets all periods with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _periodService.GetAllAsync();
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific period.
        /// </summary>
        /// <param name="id">The ID of the period to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _periodService.GetByIdAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new period.
        /// </summary>
        /// <param name="model">The period details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PeriodModel model)
        {
            try
            {
                var response = await _periodService.InsertAsync(model);
                return CreatedResponse(response, nameof(GetByIdAsync), 
                    new { id = response.Data?.Id ?? Guid.Empty });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing period.
        /// </summary>
        /// <param name="id">The ID of the period to update.</param>
        /// <param name="model">The updated period details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] PeriodModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var response = await _periodService.UpdateAsync(model);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes a period.
        /// </summary>
        /// <param name="id">The ID of the period to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _periodService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all education program rates for a specific period.
        /// </summary>
        /// <param name="id">The ID of the period.</param>
        [HttpGet("{id}/education-rates")]
        public async Task<IActionResult> GetEducationRatesAsync(Guid id)
        {
            try
            {
                var response = await _periodService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var period = response.Data;
                return Ok(period.EducationPeriodRates);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all SPSA cases active in a specific period.
        /// </summary>
        /// <param name="id">The ID of the period.</param>
        [HttpGet("{id}/cases")]
        public async Task<IActionResult> GetCasesAsync(Guid id)
        {
            try
            {
                var response = await _periodService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var period = response.Data;
                return Ok(period.SpsaCases);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}