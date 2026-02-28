namespace mini_ecommerce.Application.DTOs;

public record CreateOrderDto(string CustomerName, List<CreateOrderItemDto> Items);