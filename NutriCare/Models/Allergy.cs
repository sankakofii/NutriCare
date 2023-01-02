namespace NutriCare.Models
{
    public class Allergy
    {
        public Allergy()
        {
            this.Accounts = new HashSet<Account>();
        }

        public int AllergyId { get; set; }
        public string Type { get; set; } = string.Empty;


        public virtual ICollection<Account>? Accounts { get; set; }
    }
}