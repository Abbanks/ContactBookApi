using ContactPageApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactPageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(IRoleService roleService, RoleManager<IdentityRole> roleManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
        }

        [HttpPost("assign-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleName))
            {
                return BadRequest("Please provide user ID and role name");
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                return BadRequest("Role does not exist");
            }

            if (await _roleService.AssignRole(userId, roleName))
            {
                return Ok("Role assigned successfully");
            }

            return NotFound("User not found");
        }

        [HttpPost("add-claim")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddClaim(string userId, string claimType, string claimValue)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(claimType) || string.IsNullOrEmpty(claimValue))
            {
                return BadRequest("Please provide all required information");
            }

            if (await _roleService.AddClaim(userId, claimType, claimValue))
            {
                return Ok("Claim added successfully");
            }

            return NotFound("User not found");
        }
    }
}
