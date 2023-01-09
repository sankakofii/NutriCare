using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriCare.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string? Allergens { get; set; }
        public string? AllergensFromIngredients { get; set; }
        public string? ProductName { get; set; }
        public string? ImageFrontUrl { get; set; }
        public string? ImageNutritionUrl { get; set; }
        public string? IngredientsText { get; set; }
    }
}