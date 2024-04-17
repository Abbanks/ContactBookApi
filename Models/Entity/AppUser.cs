using Microsoft.AspNetCore.Identity;

namespace ContactPageApi.Models.Entity
{
    public class AppUser : IdentityUser
    {

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string PublicId { get; set; } = "";
        public string PhotoUrl { get; set; } = "";


    }
}
