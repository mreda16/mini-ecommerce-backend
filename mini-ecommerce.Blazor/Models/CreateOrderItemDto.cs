namespace mini-ecommerce.Blazor.Models;

public class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}