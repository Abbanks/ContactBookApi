using ContactPageApi.Models.Dto;
using ContactPageApi.Models.Entity;
using ContactPageApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactPageApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<AppUser> userManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<bool> RegisterUser(RegisterUser user)
        {
           
            var appUser = new AppUser
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };

            var result = await _userManager.CreateAsync(appUser, user.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "regular");
 
                await _userManager.AddClaimAsync(appUser, new Claim("CanUpdateDetails", "true"));
                await _userManager.AddClaimAsync(appUser, new Claim("CanGetSingleContactById", "true"));
                await _userManager.AddClaimAsync(appUser, new Claim("CanGetExistingContactsBySearchTerm", "true"));
            }

            return result.Succeeded;
        }

        public async Task<bool> LoginUser(LoginUser user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);

            if (appUser == null)
            {
                return false;
            }

            var result = await _userManager.CheckPasswordAsync(appUser, user.Password);

            return result;
        }

        public async Task<string> GenerateTokenString(LoginUser user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var roles = await _userManager.GetRolesAsync(appUser);
            if (roles != null && roles.Any())
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value);
            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredentials
                );


            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }



    }
}
