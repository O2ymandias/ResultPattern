using Microsoft.EntityFrameworkCore;
using ResultPattern.Core.Interfaces;
using ResultPattern.Infrastructure.Data;

namespace ResultPattern.Infrastructure;

public class Repository<TEntity>(AppDbContext dbContext) : IRepository<TEntity> where TEntity : class
{
    public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity>? specs = null)
    {
        return specs is null
            ? await dbContext.Set<TEntity>().ToListAsync()
            : SpecificationsEvaluator.Apply(dbContext.Set<TEntity>(), specs);
    }

    public async Task<TEntity?> GetAsync(ISpecifications<TEntity> specs)
    {
        if (specs.Criteria is null) return null;

        var entity = dbContext.Set<TEntity>().Local
            .AsQueryable()
            .FirstOrDefault(specs.Criteria);
        if (entity is not null) return entity;

        return await SpecificationsEvaluator
            .Apply(dbContext.Set<TEntity>(), specs)
            .FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(ISpecifications<TEntity>? specs = null)
    {
        var query = dbContext.Set<TEntity>();
        return specs is null
            ? await query.CountAsync()
            : await SpecificationsEvaluator.Apply(query, specs).CountAsync();
    }

    public void Add(TEntity entity)
    {
        dbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }
}