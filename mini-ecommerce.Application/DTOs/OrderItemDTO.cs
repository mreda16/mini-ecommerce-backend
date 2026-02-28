namespace mini_ecommerce.Application.DTOs;

public record OrderItemDto(
    Guid ProductId,
    string ProductName,
    decimal Price,
    int Quantity,
    decimal Subtotal);