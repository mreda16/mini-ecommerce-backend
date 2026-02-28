namespace mini_ecommerce.Application.DTOs;

public record CreateOrderDTO(string CustomerName, List<CreateOrderItemDTO> Items);