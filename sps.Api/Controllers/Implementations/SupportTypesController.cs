using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using Mapster;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing support types
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SupportTypesController : BaseController<SupportTypesController>
    {
        private readonly ISupportTypeService _supportTypeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportTypesController"/> class.
        /// </summary>
        /// <param name="supportTypeService">The support type service.</param>
        /// <param name="logger">The logger.</param>
        public SupportTypesController(ISupportTypeService supportTypeService, ILogger<SupportTypesController> logger)
            : base(logger)
        {
            _supportTypeService = supportTypeService;
        }

        /// <summary>
        /// Gets all support types with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _supportTypeService.GetAllAsync();
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific support type.
        /// </summary>
        /// <param name="id">The ID of the support type to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _supportTypeService.GetByIdAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new support type.
        /// </summary>
        /// <param name="model">The support type details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] SupportTypeModel model)
        {
            try
            {
                var response = await _supportTypeService.InsertAsync(model);
                return CreatedResponse(response, nameof(GetByIdAsync), 
                    new { id = response.Data?.Id ?? Guid.Empty });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing support type.
        /// </summary>
        /// <param name="id">The ID of the support type to update.</param>
        /// <param name="model">The updated support type details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SupportTypeModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var response = await _supportTypeService.UpdateAsync(model);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes a support type.
        /// </summary>
        /// <param name="id">The ID of the support type to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _supportTypeService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all SPSA cases using a specific support type.
        /// </summary>
        /// <param name="id">The ID of the support type.</param>
        [HttpGet("{id}/cases")]
        public async Task<IActionResult> GetCasesAsync(Guid id)
        {
            try
            {
                var response = await _supportTypeService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var supportType = response.Data;
                return Ok(supportType.SpsaCases);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets payment statistics by period for a specific support type.
        /// </summary>
        /// <param name="id">The ID of the support type.</param>
        [HttpGet("{id}/payment-stats")]
        public async Task<IActionResult> GetPaymentStatsAsync(Guid id)
        {
            try
            {
                var response = await _supportTypeService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                // This endpoint may need to be reimplemented based on your model structure
                return Ok(new { message = "This endpoint may need to be reimplemented based on your model structure" });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}