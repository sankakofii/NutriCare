using Microsoft.EntityFrameworkCore;

namespace NutriCare.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<ScanHistory> ScanHistories { get; set; }
    }
}
