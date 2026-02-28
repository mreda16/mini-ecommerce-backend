using mini_ecommerce.Domain.Common;
namespace mini_ecommerce.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    private Product() { }

    public static Result<Product> Create(string name, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Product>.Failure("Name is required");

        if (price <= 0)
            return Result<Product>.Failure("Price must be greater than zero");

        if (quantity < 0)
            return Result<Product>.Failure("Quantity cannot be negative");

        return Result<Product>.Success(new Product
        {
            Name = name,
            Price = price,
            Quantity = quantity
        });
    }

    public Result<bool> ReduceStock(int amount)
    {
        if (amount > Quantity)
            return Result<bool>.Failure("Insufficient stock");

        Quantity -= amount;
        return Result<bool>.Success(true);
    }
}