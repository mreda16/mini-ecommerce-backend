using Microsoft.AspNetCore.Mvc;
using mini_ecommerce.Application.Services;
using mini_ecommerce.Application.DTOs;

namespace mini_ecommerce.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _service;

    public OrdersController(OrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var result = await _service.Create(dto);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var order = await _service.Get(id);

        if (order == null)
            return NotFound();

        return Ok(new
        {
            order.Id,
            order.CustomerName,
            Items = order.Items,
            Subtotal = order.Subtotal(),
            Discount = order.Discount(),
            Total = order.Total()
        });
    }
}