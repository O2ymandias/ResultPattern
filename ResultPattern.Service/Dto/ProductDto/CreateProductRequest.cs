using System.ComponentModel.DataAnnotations;

namespace ResultPattern.Service.Dto.ProductDto;

public record CreateProductRequest
{
    [Required] [MaxLength(50)] public string Name { get; set; }

    [Required] [MaxLength(250)] public string Description { get; set; }

    [Required] [Range(1, int.MaxValue)] public decimal Price { get; set; }

    [Required] [Range(1, int.MaxValue)] public int UnitsSold { get; set; }
}