using mini_ecommerce.Domain.Common;
namespace mini_ecommerce.Domain.Entities;
// using mini_ecommerce.Domain.ValueObjects;

public class Order
{
    public Guid Id { get; private set; }
    public string CustomerName { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();

    public decimal Discount { get; private set; }
    public decimal Total { get; private set; }

    private Order() { }

    private Order(string customerName)
    {
        Id = Guid.NewGuid();
        CustomerName = customerName;
    }

    public static Result<Order> Create(string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            return Result<Order>.Failure("Customer name is required");

        return Result<Order>.Success(new Order(customerName));
    }

    public Result<bool> AddItem(Product product, int quantity)
    {
        var stockResult = product.ReduceStock(quantity);
        if (!stockResult.IsSuccess)
            return Result<bool>.Failure(stockResult.Error);

        Items.Add(new OrderItem(product.Id, product.Name, product.Price, quantity));
        return Result<bool>.Success(true);
    }

    public void CalculateTotals()
    {
        var subtotal = Items.Sum(x => x.Price * x.Quantity);
        var totalItems = Items.Sum(x => x.Quantity);

        if (totalItems >= 2 && totalItems <= 4)
            Discount = subtotal * 0.05m;
        else if (totalItems >= 5)
            Discount = subtotal * 0.10m;

        Total = subtotal - Discount;
    }
}