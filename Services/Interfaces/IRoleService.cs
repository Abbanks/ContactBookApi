namespace ContactPageApi.Services.Interfaces
{
    public interface IRoleService
    {
        Task<bool> AssignRole(string userId, string roleName);
        Task<bool> AddClaim(string userId, string claimType, string claimValue);


    }
}
