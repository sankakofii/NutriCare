using Microsoft.EntityFrameworkCore;

namespace NutriCare.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Scan> Scans { get; set; }

        public DbSet<Allergy> Allergies { get; set; }

        public DbSet<Intolerance> Intolerances { get; set; }

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().HasIndex(u => u.Barcode).IsUnique();
        }

    }
}
