using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResultPattern.Core.Models;

namespace ResultPattern.Infrastructure.Data.Config;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(250);
        builder
            .Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder
            .Property(p => p.UnitsSold)
            .IsRequired();
    }
}