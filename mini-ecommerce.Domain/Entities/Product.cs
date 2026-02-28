using mini_ecommerce.Domain.Common;
// using mini_ecommerce.Domain.ValueObjects;
namespace mini_ecommerce.Domain.Entities;


public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    private Product() { }

    private Product(string name, decimal price, int quantity)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public static Result<Product> Create(string name, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Product>.Failure("Name is required");

        if (price <= 0)
            return Result<Product>.Failure("Price must be greater than zero");

        if (quantity < 0)
            return Result<Product>.Failure("Quantity must be >= 0");

        return Result<Product>.Success(new Product(name, price, quantity));
    }

    public Result<bool> ReduceStock(int amount)
    {
        if (amount <= 0)
            return Result<bool>.Failure("Invalid quantity");

        if (Quantity < amount)
            return Result<bool>.Failure("Insufficient stock");

        Quantity -= amount;
        return Result<bool>.Success(true);
    }
}