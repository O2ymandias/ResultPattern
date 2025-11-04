using ResultPattern.Core.Models;

namespace ResultPattern.Core.Specifications.ProductSpecifications;

public class ProductsSpecifications : Specifications<Product>
{
    public ProductsSpecifications(int pageIndex, int pageSize)
    {
        ApplyPagination(pageIndex, pageSize);
    }

    public ProductsSpecifications(int id)
    {
        ApplyFiltration(p => p.Id == id);
    }

    public ProductsSpecifications(string name)
    {
        var normalizedName = name.ToUpper();
        ApplyFiltration(p => p.Name.ToUpper().Equals(normalizedName));
    }
}