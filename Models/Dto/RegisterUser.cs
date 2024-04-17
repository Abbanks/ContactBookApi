using System.ComponentModel.DataAnnotations;

namespace ContactPageApi.Models.Dto
{
    public class RegisterUser
    {

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = "";
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = "";


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; } = "";

        [RegularExpression(@"234-[0-9]{10}", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one digit")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
}
