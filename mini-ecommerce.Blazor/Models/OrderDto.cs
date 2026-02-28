namespace mini-ecommerce.Blazor.Models;

public class OrderDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = "";
    public List<OrderItemDto> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}