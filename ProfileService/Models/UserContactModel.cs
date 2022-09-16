namespace ProfileService.Models
{
    public class UserContactModel
    {
        public string UserName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ContactPreference { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
    }
}
