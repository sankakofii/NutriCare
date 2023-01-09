using NutriCare.Models;

namespace NutriCare.DTOs
{
    public class ResponseDTO
    {
        public string code { get; set; }
        public int status { get; set; } = 2;
        public string status_verbose { get; set; } = "product found in NCDB";
        public ProductDTO product { get; set; }
        public string harm { get; set; } = string.Empty;
    }
}