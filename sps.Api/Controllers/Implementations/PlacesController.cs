using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using Mapster;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing places/departments
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : BaseController<PlacesController>
    {
        private readonly IPlaceService _placeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlacesController"/> class.
        /// </summary>
        /// <param name="placeService">The place service.</param>
        /// <param name="logger">The logger.</param>
        public PlacesController(IPlaceService placeService, ILogger<PlacesController> logger)
            : base(logger)
        {
            _placeService = placeService;
        }

        /// <summary>
        /// Gets all places with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _placeService.GetAllAsync();
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific place.
        /// </summary>
        /// <param name="id">The ID of the place to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _placeService.GetByIdAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new place.
        /// </summary>
        /// <param name="model">The place details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PlaceModel model)
        {
            try
            {
                var response = await _placeService.InsertAsync(model);
                return CreatedResponse(response, nameof(GetByIdAsync), 
                    new { id = response.Data?.Id ?? Guid.Empty });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing place.
        /// </summary>
        /// <param name="id">The ID of the place to update.</param>
        /// <param name="model">The updated place details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] PlaceModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var response = await _placeService.UpdateAsync(model);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes a place.
        /// </summary>
        /// <param name="id">The ID of the place to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _placeService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all teachers assigned to a specific place.
        /// </summary>
        /// <param name="id">The ID of the place.</param>
        [HttpGet("{id}/teachers")]
        public async Task<IActionResult> GetTeachersAsync(Guid id)
        {
            try
            {
                var response = await _placeService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var place = response.Data;
                return Ok(place.SupportingTeachers);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets workload statistics for teachers at a specific place.
        /// </summary>
        /// <param name="id">The ID of the place.</param>
        [HttpGet("{id}/teacher-workloads")]
        public async Task<IActionResult> GetTeacherWorkloadsAsync(Guid id)
        {
            try
            {
                var response = await _placeService.GetByIdAsync(id);
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