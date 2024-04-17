using ContactPageApi.Models.Entity;
using ContactPageApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ContactPageApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<AppUser> _userManager;

        public RoleService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return result.Succeeded;
        }
        public async Task<bool> AddClaim(string userId, string claimType, string claimValue)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));

            return result.Succeeded;
        }


    }
}
