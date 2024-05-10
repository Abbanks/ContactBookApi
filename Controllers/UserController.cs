using ContactPageApi.Models.Dto;
using ContactPageApi.Models.Entity;
using ContactPageApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactPageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
      
        public UserController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "admin,regular")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _userService.GetUserById(id);

                if (user != null)
                {
                    return Ok(user);
                }

                return NotFound("User not found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search")]
        [Authorize(Roles = "admin,regular")]
        public IActionResult GetUsersBySearchTerm([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty");
            }

            var users = _userService.GetUsersBySearchTerm(searchTerm).ToList();

            if (users == null || !users.Any())
            {
                return NotFound("No results found for search term");
            }

            return Ok(users);
        }

        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetPagedContacts([FromQuery] int pageNumber)
        {
            
            int pageSize = 10;

            try
            {
                var pagedContacts = await _userService.GetPagedContacts(pageNumber, pageSize);
                return Ok(pagedContacts);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "regular,admin")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUser user)
        {
         
           //var currentUser = await _userManager.GetUserAsync(User);

            var currentUser = HttpContext.User.Identity as ClaimsIdentity;
            var emailClaim = currentUser.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;   
            var appUser = await _userManager.FindByEmailAsync(emailClaim);

            if (currentUser == null)
            {
                return Unauthorized(); 
            }
 
            if (id != appUser.Id)
            {
                return Forbid(); 
            }

            var updated = await _userService.UpdateUser(id, user);

            if (updated)
            {
                return Ok("User updated");
            }

            return NotFound("User not found");
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var deleted = await _userService.DeleteUser(id);

            if (deleted)
            {
                return Ok("User deleted");
            }

            return NotFound("User not found");
        }

        [HttpPatch("photo/{id}")]
        [Authorize(Roles = "regular,admin")]
        public async Task<IActionResult> AddPhoto(string id, IFormFile file)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var currentUser = HttpContext.User.Identity as ClaimsIdentity;
            var emailClaim = currentUser.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            var appUser = await _userManager.FindByEmailAsync(emailClaim);

            if (currentUser == null)
            {
                return Unauthorized();
            }

            if (id != appUser.Id)
            {
                return Forbid();
            }


            var photo = await _userService.AddPhoto(id, file);

            if (photo != null)
            {
                return Ok(photo);
            }
            else
            {
                return BadRequest("Failed to add photo");
            }
        }


    }
}
