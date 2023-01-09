namespace NutriCare.DTOs
{
    public class AuthAccountDTO
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public AccountDTO Account { get; set; } = null!;
    }
}
