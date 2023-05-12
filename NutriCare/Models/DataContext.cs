using Microsoft.EntityFrameworkCore;
using NutriCare.Models;

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

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<IntoleranceIngredient> IntoleranceIngredient { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Product
            builder.Entity<Product>()
                .HasIndex(u => u.Barcode).IsUnique();

            //Account
            builder.Entity<Account>()
                .HasOne(a => a.RefreshToken)
                .WithOne(a => a.Account)
                .HasForeignKey<RefreshToken>(c => c.AccountId);

        }


        

    }
}
