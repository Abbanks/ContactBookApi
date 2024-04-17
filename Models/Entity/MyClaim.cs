namespace ContactPageApi.Models.Entity
{
    public class MyClaim
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "";
    }
}
