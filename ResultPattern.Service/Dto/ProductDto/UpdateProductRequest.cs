using System.ComponentModel.DataAnnotations;

namespace ResultPattern.Service.Dto.ProductDto;

public record UpdateProductRequest : CreateProductRequest
{
    [Required] [Range(1, int.MaxValue)] public int Id { get; set; }
}