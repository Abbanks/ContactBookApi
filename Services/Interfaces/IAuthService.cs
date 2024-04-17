using ContactPageApi.Models.Dto;

namespace ContactPageApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateTokenString(LoginUser user);
        Task<bool> LoginUser(LoginUser user);
        Task<bool> RegisterUser(RegisterUser user);

    }
}

