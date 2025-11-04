using ResultPattern.Core.Interfaces;
using ResultPattern.Infrastructure.Data;

namespace ResultPattern.Infrastructure;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = [];

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var key = typeof(TEntity);

        if (_repositories.TryGetValue(key, out var repo)) return (IRepository<TEntity>)repo;
        var newRepo = new Repository<TEntity>(dbContext);
        _repositories.Add(key, newRepo);
        return newRepo;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}