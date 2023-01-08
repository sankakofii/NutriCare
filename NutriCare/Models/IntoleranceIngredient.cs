namespace NutriCare.Models
{
    public class IntoleranceIngredient
    {
        public int IntoleranceIngredientId { get; set; }
        public string IntoleranceIngredientName { get; set; } = string.Empty;
        public int IntoleranceId { get; set; }
        public Intolerance? Intolerance { get; set; }
    }
}
