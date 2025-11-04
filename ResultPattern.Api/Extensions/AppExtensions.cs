using Microsoft.EntityFrameworkCore;
using ResultPattern.Infrastructure.Data;

namespace ResultPattern.Api.Extensions;

public static class AppExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
            await DatabaseInitializer.SeedDataAsync(dbContext);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}