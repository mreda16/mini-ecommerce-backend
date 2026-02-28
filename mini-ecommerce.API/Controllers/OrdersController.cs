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

    // US-03 Create Order
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDTO request)
    {
        var result = await _service.CreateAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    // US-05 Get Order Details
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }
}