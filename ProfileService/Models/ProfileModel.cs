namespace ProfileService.Models
{
    public class ProfileModel
    {
        public int ProfileId { get; set; } 
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime Dob { get; set; }
        public string IdType { get; set; } = null!;
        public string IdValue { get; set; } = null!;
    }
}
