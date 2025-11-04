using System.Text.Json;
using ResultPattern.Core.Models;

namespace ResultPattern.Infrastructure.Data;

public static class DatabaseInitializer
{
    public static async Task SeedDataAsync(AppDbContext dbContext)
    {
        var filePath = "../ResultPattern.Infrastructure/Data/SeedData/products.json";

        if (!File.Exists(filePath)) return;

        var jsonData = await File.ReadAllTextAsync(filePath);

        var products = JsonSerializer.Deserialize<List<Product>>(jsonData);

        if (products is null || products.Count == 0) return;

        if (dbContext.Products.Any()) return;

        dbContext.Products.AddRange(products);

        await dbContext.SaveChangesAsync();
    }
}