using System.Linq.Expressions;

namespace ResultPattern.Core.Interfaces;

public interface ISpecifications<T> where T : class
{
    public Expression<Func<T, bool>>? Criteria { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }
    public bool IsPaginationEnabled { get; set; }
}