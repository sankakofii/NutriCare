using NutriCare.Models;

namespace NutriCare.DTOs
{
    public class ScanDTO
    {
        public string code { get; set; }
        public int ScanId { get; set; }
        public DateTime ScanTime { get; set; }
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public ProductDTO product { get; set; }
    }
}
