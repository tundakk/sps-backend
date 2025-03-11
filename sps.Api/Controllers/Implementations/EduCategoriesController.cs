using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using Mapster;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing education categories
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EduCategoriesController : BaseController<EduCategoriesController>
    {
        private readonly IEduCategoryService _eduCategoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EduCategoriesController"/> class.
        /// </summary>
        /// <param name="eduCategoryService">The education category service.</param>
        /// <param name="logger">The logger.</param>
        public EduCategoriesController(IEduCategoryService eduCategoryService, ILogger<EduCategoriesController> logger)
            : base(logger)
        {
            _eduCategoryService = eduCategoryService;
        }

        /// <summary>
        /// Gets all education categories with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _eduCategoryService.GetAllAsync();
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific education category.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _eduCategoryService.GetByIdAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new education category.
        /// </summary>
        /// <param name="model">The category details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] EduCategoryModel model)
        {
            try
            {
                var response = await _eduCategoryService.InsertAsync(model);
                return CreatedResponse(response, nameof(GetByIdAsync), 
                    new { id = response.Data?.Id ?? Guid.Empty });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing education category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="model">The updated category details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EduCategoryModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var response = await _eduCategoryService.UpdateAsync(model);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes an education category.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _eduCategoryService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all education programs in a specific category.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        [HttpGet("{id}/educations")]
        public async Task<IActionResult> GetEducationsAsync(Guid id)
        {
            try
            {
                var response = await _eduCategoryService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var category = response.Data;
                return Ok(category.Educations);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all SPSA cases in a specific category.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        [HttpGet("{id}/cases")]
        public async Task<IActionResult> GetCasesAsync(Guid id)
        {
            try
            {
                var response = await _eduCategoryService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var category = response.Data;
                return Ok(category.SpsaCases);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets support statistics by period for a specific category.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        [HttpGet("{id}/support-stats")]
        public async Task<IActionResult> GetSupportStatsAsync(Guid id)
        {
            try
            {
                var response = await _eduCategoryService.GetByIdAsync(id);
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