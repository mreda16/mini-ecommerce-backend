namespace mini_ecommerce.Domain.Entities;

public class OrderItem
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    private OrderItem() { }

    public OrderItem(Guid productId, string name, decimal price, int quantity)
    {
        ProductId = productId;
        ProductName = name;
        Price = price;
        Quantity = quantity;
    }
}