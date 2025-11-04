namespace ResultPattern.Core.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity>? specs = null);
    Task<TEntity?> GetAsync(ISpecifications<TEntity> specs);
    Task<int> CountAsync(ISpecifications<TEntity>? specs = null);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}