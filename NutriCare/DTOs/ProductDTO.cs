namespace NutriCare.DTOs
{
    public class ProductDTO
    {
        public string _id { get; set; }
        public string allergens { get; set; }
        public string allergens_from_ingredients { get; set; }
        public string product_name { get; set; }
        public string image_front_url { get; set; }
        public string image_nutrition_url { get; set; }
        public string ingredients_text { get; set; }
    }
}
