using NutriCare.Models;

namespace NutriCare.DTOs
{
    public class CreateScanHistoryDTO
    {
        public DateTime ScanTime { get; set; }
        public Account Account { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
