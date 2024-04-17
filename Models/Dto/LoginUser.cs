using System.ComponentModel.DataAnnotations;

namespace ContactPageApi.Models.Dto
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one digit")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";


    }
}



