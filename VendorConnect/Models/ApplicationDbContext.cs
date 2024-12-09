using Microsoft.EntityFrameworkCore;
using VendorConnect.Models;

namespace VendorConnect
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor to configure DbContext with options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets for the various entities in the database
        public DbSet<Account> AccountTable { get; set; }
        public DbSet<Vendor> VendorTable { get; set; }
        public DbSet<Product> Product { get; set; }

        // Configuring the model and explicitly setting primary keys and relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly set primary keys if not using data annotations
            modelBuilder.Entity<Account>()
                        .HasKey(a => a.UserId);  // Ensuring 'UserId' is treated as the primary key for Account

            modelBuilder.Entity<Vendor>()
                        .HasKey(v => v.VendorId);  // Ensuring 'VendorId' is treated as the primary key for Vendor

            modelBuilder.Entity<Product>()
                        .HasKey(p => p.ProductId);  // Ensuring 'ProductId' is treated as the primary key for Product

            // Optionally, you can define relationships between entities if necessary
            modelBuilder.Entity<Product>()
                        .HasOne(p => p.Vendor)  // Define relationship: Product has one Vendor
                        .WithMany(v => v.Products)  // Vendor can have many Products
                        .HasForeignKey(p => p.VendorId); // Foreign key for Vendor in Product
        }
    }
}
