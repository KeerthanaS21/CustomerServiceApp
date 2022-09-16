namespace AccountService.Models
{
    public class RegisterAccount
    {
        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public int UserRole { get; set; }
        public string UserEmail { get; set; } = null!;
    }
}
