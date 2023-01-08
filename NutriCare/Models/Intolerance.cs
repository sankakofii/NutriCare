namespace NutriCare.Models
{
    public class Intolerance
    {
        public Intolerance()
        {
            this.Accounts = new HashSet<Account>();
        }
        public int IntoleranceId { get; set; }
        public string Type { get; set; } = string.Empty;


        public virtual ICollection<Account>? Accounts { get; set; }
        public virtual ICollection<IntoleranceIngredient>? IntoleranceIngredients { get; set; }
    }
}