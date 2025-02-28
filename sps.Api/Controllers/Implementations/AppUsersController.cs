using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace sps.API.Controllers
{
    /// <summary>
    /// Controller for managing application users.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AppUsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<AppUsersController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUsersController"/> class.
        /// </summary>
        public AppUsersController(
            UserManager<IdentityUser<Guid>> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ILogger<AppUsersController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        /// <summary>
        /// Gets all users with pagination.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var users = await _userManager.Users
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users");
                return StatusCode(500, "An error occurred while fetching users.");
            }
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user by id");
                return StatusCode(500, "An error occurred while fetching the user.");
            }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="model">The user creation data.</param>
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            var user = new IdentityUser<Guid>
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { user.Id, user.UserName, user.Email });
            }

            return BadRequest(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok("User deleted successfully.");
                }

                return BadRequest(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user");
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }

        /// <summary>
        /// Assigns a role to a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="role">The role to assign.</param>
        [Authorize(Roles = "Admin")]
        [HttpPost("{userId}/assign-role")]
        public async Task<IActionResult> AssignRole(Guid userId, [FromBody] string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                return BadRequest("Role does not exist");
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                return Ok("Role assigned successfully");
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Removes a role from a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="role">The role to remove.</param>
        [Authorize(Roles = "Admin")]
        [HttpPost("{userId}/remove-role")]
        public async Task<IActionResult> RemoveRole(Guid userId, [FromBody] string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                return BadRequest("Role does not exist");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role);
            if (result.Succeeded)
            {
                return Ok("Role removed successfully");
            }

            return BadRequest(result.Errors);
        }
    }

    /// <summary>
    /// Request model for creating a new user.
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}