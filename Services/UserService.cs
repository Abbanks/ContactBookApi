using ContactPageApi.Models.Dto;
using ContactPageApi.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContactPageApi.Services.Interfaces
{
    public class UserService : Mappers, IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoService _photoService;
        public UserService(UserManager<AppUser> userManager, IPhotoService photoService)
        {
            _userManager = userManager;
            _photoService = photoService;
        }

        public async Task<UserResponse> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return MapToResponse(user);
        }

        public IEnumerable<UserResponse> GetAllUsers()
        {
            var users = _userManager.Users.Select(MapToResponse);
            return users;
        }

        public IEnumerable<UserResponse> GetUsersBySearchTerm(string searchTerm)
        {
            var users = _userManager.Users.Where(x => x.FirstName.Contains(searchTerm)
            || x.LastName.Contains(searchTerm)).Select(MapToResponse);

            return users;
        }

        public async Task<PagedContacts> GetPagedContacts(int pageNumber, int pageSize)
        {

            var totalContacts = await _userManager.Users.CountAsync();

            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Page number and page size must be greater than 0");
            }

            var users = await _userManager.Users
                .OrderBy(u => u.UserName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mappedUsers = users.Select(MapToResponse);

            return new PagedContacts
            {
                Contacts = mappedUsers,
                Total = totalContacts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalContacts / pageSize)
            };
        }

        public async Task<bool> UpdateUser(string userId, UpdateUser user)
        {

            var appUser = await _userManager.FindByIdAsync(userId);

            if (appUser == null)
            {
                return false;
            }

            appUser.FirstName = user.FirstName;
            appUser.LastName = user.LastName;
            appUser.PhoneNumber = user.PhoneNumber;

            var result = await _userManager.UpdateAsync(appUser);

            return result.Succeeded;
        }
        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }


        public async Task<UserResponse?> AddPhoto(string id, IFormFile file)
        {
            var user = await _userManager.FindByIdAsync(id);

            var result = await _photoService.AddPhotoAsync(file);

            if (result == null)
            {
                return null;
            }

            user.PublicId = result.PublicId;
            user.PhotoUrl = result.SecureUrl.AbsoluteUri;

            await _userManager.UpdateAsync(user);

            return MapToResponse(user);
        }
    }




}

