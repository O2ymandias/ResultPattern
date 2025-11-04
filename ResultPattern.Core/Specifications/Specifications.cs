using System.Linq.Expressions;
using ResultPattern.Core.Interfaces;

namespace ResultPattern.Core.Specifications;

public abstract class Specifications<T> : ISpecifications<T>
    where T : class
{
    protected Specifications()
    {
    }

    protected Specifications(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>>? Criteria { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }
    public bool IsPaginationEnabled { get; set; }

    protected void ApplyPagination(int pageIndex, int pageSize)
    {
        IsPaginationEnabled = true;
        Take = pageSize;
        Skip = (pageIndex - 1) * pageSize;
    }

    protected void ApplyFiltration(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
}