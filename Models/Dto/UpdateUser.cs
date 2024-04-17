using System.ComponentModel.DataAnnotations;

namespace ContactPageApi.Models.Dto
{
    public class UpdateUser
    {

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = "";
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = "";

        public string PhoneNumber { get; set; } = "";
    }
}
