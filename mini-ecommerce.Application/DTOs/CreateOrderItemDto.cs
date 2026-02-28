namespace mini_ecommerce.Application.DTOs;

public record CreateOrderItemDTO(Guid ProductId, int Quantity);