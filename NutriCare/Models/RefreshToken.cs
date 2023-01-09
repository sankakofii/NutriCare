using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace NutriCare.Models
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
