using ResultPattern.Core.Errors;
using ResultPattern.Core.Interfaces;
using ResultPattern.Core.Models;
using ResultPattern.Core.Results;
using ResultPattern.Core.Shared;
using ResultPattern.Core.Specifications.ProductSpecifications;
using ResultPattern.Service.Dto.ProductDto;
using ResultPattern.Service.Interfaces;

namespace ResultPattern.Service.Implementations;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task<Result<PagedResult<ProductResponse>>> GetAllProductsAsync(PaginationParams paginationParams)
    {
        var validationResult = paginationParams.Validate();
        if (!validationResult.IsSuccess) return Result<PagedResult<ProductResponse>>.Failure(validationResult.Error!);

        var productRepo = unitOfWork.Repository<Product>();
        var specs = new ProductsSpecifications(paginationParams.Page, paginationParams.PageSize);
        var products = await productRepo.GetAllAsync(specs);
        IReadOnlyList<ProductResponse> items =
        [
            .. products.Select(product =>
                new ProductResponse(product.Name, product.Description, product.Price, product.UnitsSold))
        ];
        var count = await productRepo.CountAsync();
        var pagination = new PaginationMetadata(paginationParams.Page, paginationParams.PageSize, count);
        var pagedResult = new PagedResult<ProductResponse>(items, pagination);
        return Result<PagedResult<ProductResponse>>.Success(pagedResult);
    }

    public async Task<Result<ProductResponse>> GetProductByIdAsync(int id)
    {
        var product = await unitOfWork
            .Repository<Product>()
            .GetAsync(new ProductsSpecifications(id));

        return product is not null
            ? Result<ProductResponse>.Success(new ProductResponse(product.Name, product.Description, product.Price,
                product.UnitsSold))
            : Result<ProductResponse>.Failure(ProductErrors.NotFound(id));
    }

    public async Task<Result> CreateProductAsync(CreateProductRequest requestDate)
    {
        var productRepo = unitOfWork.Repository<Product>();

        if (requestDate.Price <= 0)
            return Result.Failure(ProductErrors.InvalidPrice(requestDate.Price));

        var alreadyExistedName = await productRepo.GetAsync(new ProductsSpecifications(requestDate.Name));
        if (alreadyExistedName is not null)
            return Result.Failure(ProductErrors.AlreadyExists(requestDate.Name));

        var newProduct = new Product
        {
            Name = requestDate.Name,
            Description = requestDate.Description,
            Price = requestDate.Price,
            UnitsSold = requestDate.UnitsSold
        };

        productRepo.Add(newProduct);
        var rowsAffected = await unitOfWork.SaveChangesAsync();

        return rowsAffected > 0
            ? Result.Success()
            : Result.Failure(ProductErrors.CreationFailed());
    }

    public async Task<Result> UpdateProductAsync(UpdateProductRequest requestDate)
    {
        var existedProduct = await unitOfWork
            .Repository<Product>()
            .GetAsync(new ProductsSpecifications(requestDate.Id));
        if (existedProduct is null)
            return Result.Failure(ProductErrors.NotFound(requestDate.Id));

        if (requestDate.Price <= 0)
            return Result.Failure(ProductErrors.InvalidPrice(requestDate.Price));

        existedProduct.Name = requestDate.Name;
        existedProduct.Description = requestDate.Description;
        existedProduct.Price = requestDate.Price;
        existedProduct.UnitsSold = requestDate.UnitsSold;

        var rowsAffected = await unitOfWork.SaveChangesAsync();

        return rowsAffected > 0
            ? Result.Success()
            : Result.Failure(ProductErrors.NoChangesDetected());
    }

    public async Task<Result> DeleteProductAsync(int id)
    {
        var productRepo = unitOfWork.Repository<Product>();

        var product = await productRepo.GetAsync(new ProductsSpecifications(id));
        if (product is null) return Result.Failure(ProductErrors.NotFound(id));

        productRepo.Delete(product);
        var rowsAffected = await unitOfWork.SaveChangesAsync();
        return rowsAffected > 0
            ? Result.Success()
            : Result.Failure(ProductErrors.DeletionFailed());
    }
}