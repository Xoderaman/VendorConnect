using Microsoft.EntityFrameworkCore;
using VendorConnect.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet for UserRegistrationModel
    public DbSet<UserRegistrationModel> Users { get; set; }

    // DbSet for Product
    public DbSet<Product> Products { get; set; }  // Add this line to define Products DbSet
}
