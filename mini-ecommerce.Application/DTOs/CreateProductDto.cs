namespace mini_ecommerce.Application.DTOs;

public record CreateProductDto(string Name, decimal Price, int Quantity);