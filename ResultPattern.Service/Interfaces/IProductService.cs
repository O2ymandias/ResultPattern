using ResultPattern.Core.Results;
using ResultPattern.Core.Shared;
using ResultPattern.Service.Dto.ProductDto;

namespace ResultPattern.Service.Interfaces;

public interface IProductService
{
    public Task<Result<PagedResult<ProductResponse>>> GetAllProductsAsync(PaginationParams paginationParams);
    public Task<Result<ProductResponse>> GetProductByIdAsync(int id);
    public Task<Result> UpdateProductAsync(UpdateProductRequest requestDate);
    public Task<Result> CreateProductAsync(CreateProductRequest requestDate);
    public Task<Result> DeleteProductAsync(int id);
}