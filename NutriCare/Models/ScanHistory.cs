using System.Net.Http.Headers;

namespace NutriCare.Models
{
    public class ScanHistory
    {
        public int ScanHistoryId { get; set; }
        public DateTime ScanTime { get; set; }
        public Account Account { get; set; } = null!;
        public int AccountId { get; set; }
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }
    }
}
