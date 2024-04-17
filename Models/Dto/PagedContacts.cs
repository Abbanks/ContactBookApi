namespace ContactPageApi.Models.Dto
{
    public class PagedContacts
    {
        public int Total { get; set; }
        public IEnumerable<UserResponse> Contacts { get; set; }

        public int TotalPages { get; set; }
  
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
