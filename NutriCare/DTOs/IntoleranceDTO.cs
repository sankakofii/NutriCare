using NutriCare.Models;

namespace NutriCare.DTOs
{
    public class IntoleranceDTO
    {
        public int IntoleranceId { get; set; }
        public string Type { get; set; } = string.Empty;
        public ICollection<IntoleranceIngredient>? IntoleranceIngredients { get; set; }
    }
}
