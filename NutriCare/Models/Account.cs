using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriCare.Models
{
    public class Account
    {
        public Account()
        {
            this.Allergies = new HashSet<Allergy>();
            this.Intolerances = new HashSet<Intolerance>();
        }

        public int AccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        [MaxLength(96)]
        public byte[] Password { get; set; } = null!;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int RefreshTokenId { get; set; }
        public RefreshToken RefreshToken { get; set; } = null!;



        public virtual ICollection<Allergy>? Allergies { get; set; }
        public virtual ICollection<Intolerance>? Intolerances { get; set; }
    }
}
