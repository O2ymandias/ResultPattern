namespace ResultPattern.Service.Dto.ProductDto;

public record ProductResponse(string Name, string Description, decimal Price, int UnitsSold);