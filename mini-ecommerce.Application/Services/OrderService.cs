using mini_ecommerce.Application.Interfaces.Repositories;
using mini_ecommerce.Domain.Common;
using mini_ecommerce.Application.DTOs;
using mini_ecommerce.Application.Interfaces.Services;
using mini_ecommerce.Domain.Entities;
using mini_ecommerce.Application.Common;

namespace mini_ecommerce.Application.Services;

public class OrderService
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderService(IProductRepository productRepository,
        IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }
    
    public async Task<Result<OrderDetailsDto>> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order is null)
            return Result<OrderDetailsDto>.Failure("Order not found");

        var dto = new OrderDetailsDto(
            order.Id,
            order.CustomerName,
            order.Items.Select(i => new OrderItemDto(
                i.ProductId,
                i.ProductName,
                i.Price,
                i.Quantity,
                i.Price * i.Quantity)).ToList(),
            order.Discount,
            order.Total);

        return Result<OrderDetailsDto>.Success(dto);
    }

    public async Task<Result<Guid>> CreateAsync(CreateOrderDTO request)
    {
        var orderResult = Order.Create(request.CustomerName);
        if (!orderResult.IsSuccess)
            return Result<Guid>.Failure(orderResult.Error);

        var order = orderResult.Value;

        foreach (var item in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            if (product is null)
                return Result<Guid>.Failure("Product not found");

            var addResult = order.AddItem(product, item.Quantity);
            if (!addResult.IsSuccess)
                return Result<Guid>.Failure(addResult.Error);
        }

        order.CalculateTotals();

        await _orderRepository.AddAsync(order);

        return Result<Guid>.Success(order.Id);
    }
}