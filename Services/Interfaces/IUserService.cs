using ContactPageApi.Models.Dto;

namespace ContactPageApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserById(string id);
        IEnumerable<UserResponse> GetAllUsers();
        IEnumerable<UserResponse> GetUsersBySearchTerm(string searchTerm);
        Task<PagedContacts> GetPagedContacts(int pageNumber, int pageSize);
        Task<bool> UpdateUser(string userId, UpdateUser user);
        Task<bool> DeleteUser(string id);
        Task<UserResponse> AddPhoto(string id, IFormFile file);
 
    }
}
