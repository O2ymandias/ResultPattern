using ResultPattern.Core.Interfaces;
using ResultPattern.Infrastructure.Data;

namespace ResultPattern.Infrastructure;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        return new Repository<TEntity>(dbContext);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}