namespace mini-ecommerce.Blazor.Models;

public class CreateOrderDto
{
    public string CustomerName { get; set; } = "";
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

public class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}