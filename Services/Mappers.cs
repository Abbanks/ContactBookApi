using ContactPageApi.Models.Dto;
using ContactPageApi.Models.Entity;

namespace ContactPageApi.Services.Interfaces
{
    public class Mappers
    {
        public UserResponse MapToResponse(AppUser appUser)
        {
            return new UserResponse
            {
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email,
                PhoneNumber = appUser.PhoneNumber,
                PhotoUrl = appUser.PhotoUrl

            };
        }

 
    }
}