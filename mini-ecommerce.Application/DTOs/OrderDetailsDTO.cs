namespace mini_ecommerce.Application.DTOs;

public record OrderDetailsDto(
    Guid Id,
    string CustomerName,
    List<OrderItemDto> Items,
    decimal Discount,
    decimal Total);