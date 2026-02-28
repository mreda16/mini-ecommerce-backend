namespace mini_ecommerce.Application.DTOs;

public record CreateOrderItemDto(Guid ProductId, int Quantity);