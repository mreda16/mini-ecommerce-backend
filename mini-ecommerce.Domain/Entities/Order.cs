using mini_ecommerce.Domain.Common;
namespace mini_ecommerce.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string CustomerName { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();

    private Order() { }

    public static Result<Order> Create(string customerName, List<OrderItem> items)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            return Result<Order>.Failure("Customer name required");

        if (items.Count == 0)
            return Result<Order>.Failure("Order must contain items");

        return Result<Order>.Success(new Order
        {
            CustomerName = customerName,
            Items = items
        });
    }

    public decimal Subtotal()
        => Items.Sum(x => x.Total());

    public decimal Discount()
    {
        var itemCount = Items.Sum(x => x.Quantity);

        if (itemCount >= 5)
            return Subtotal() * 0.10m;

        if (itemCount >= 2)
            return Subtotal() * 0.05m;

        return 0;
    }

    public decimal Total()
        => Subtotal() - Discount();
}