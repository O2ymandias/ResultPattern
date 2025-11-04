using Microsoft.EntityFrameworkCore;
using ResultPattern.Core.Models;
using ResultPattern.Infrastructure.Data.Config;

namespace ResultPattern.Infrastructure.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfig());
    }
}