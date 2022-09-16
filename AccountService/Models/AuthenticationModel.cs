namespace AccountService.Models
{
    public class AuthenticationModel
    {
        public string UserName { get; set; } = null!;
        public int UserRole { get; set; }
        public string Token { get; set; } = null!;
    }
}
