namespace mini_ecommerce.Application.DTOs;

public record ProductDto(Guid Id, string Name, decimal Price, int Quantity);