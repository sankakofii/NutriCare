using NutriCare.Models;

namespace NutriCare.DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public IEnumerable<AllergyDTO>? Allergies { get; set; }
        public IEnumerable<IntoleranceDTO>? Intolerances { get; set; }
    }
}
