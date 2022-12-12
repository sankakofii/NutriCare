namespace NutriCare.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte Password { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Allergy? Allergies { get; set; }
        public Diabetes? Diabetes { get; set; }
        public Intolerance? Intolerance { get; set; }
    }
}
