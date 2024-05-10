namespace ContactPageApi.Models.Dto
{
    public class UserResponse
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";

        public string PhoneNumber { get; set; } = "";
        public string PublicId { get; set; } = "";
        public string PhotoUrl { get; set; } = "";
    }
}
