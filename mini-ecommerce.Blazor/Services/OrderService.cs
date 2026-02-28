using mini-ecommerce.Blazor.Models;

namespace mini-ecommerce.Blazor.Services;

public class OrderService
{
    private readonly ApiClient _api;

    public OrderService(ApiClient api)
    {
        _api = api;
    }

    public async Task<Guid?> Create(CreateOrderDto dto)
    {
        return await _api.Post<CreateOrderDto, Guid>("api/orders", dto);
    }

    public async Task<OrderDto?> Get(Guid id)
    {
        return await _api.Get<OrderDto>($"api/orders/{id}");
    }
}