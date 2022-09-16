namespace ProfileService.Models
{
    public class ContactModel
    {
        public int ContactId { get; set; }
        public string? UserName { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ContactPreference { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
    }
}
