using mini_ecommerce.Application.Interfaces.Repositories;
using mini_ecommerce.Domain.Common;
using mini_ecommerce.Application.DTOs;
using mini_ecommerce.Application.Interfaces.Services;
using mini_ecommerce.Domain.Entities;

namespace mini_ecommerce.Application.Services;

public class OrderService: IOrderService
{
    private readonly IProductRepository _products;
    private readonly IOrderRepository _orders;

    public OrderService(IProductRepository products, IOrderRepository orders)
    {
        _products = products;
        _orders = orders;
    }

    public async Task<Result<Guid>> Create(CreateOrderDto dto)
    {
        var items = new List<OrderItem>();

        foreach (var item in dto.Items)
        {
            var product = await _products.Get(item.ProductId);

            if (product == null)
                return Result<Guid>.Failure("Product not found");

            var stockResult = product.ReduceStock(item.Quantity);

            if (!stockResult.IsSuccess)
                return Result<Guid>.Failure(stockResult.Error);

            items.Add(new OrderItem(product.Id, product.Price, item.Quantity));
        }

        var orderResult = Order.Create(dto.CustomerName, items);

        if (!orderResult.IsSuccess)
            return Result<Guid>.Failure(orderResult.Error);

        await _orders.Add(orderResult.Value);
        await _orders.SaveChanges();
        await _products.SaveChanges();

        return Result<Guid>.Success(orderResult.Value.Id);
    }

    public async Task<Order?> Get(Guid id)
        => await _orders.Get(id);
}