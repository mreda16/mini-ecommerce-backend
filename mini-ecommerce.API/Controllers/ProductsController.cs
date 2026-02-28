using Microsoft.AspNetCore.Mvc;
using mini_ecommerce.Application.Services;
using mini_ecommerce.Application.DTOs;

namespace mini_ecommerce.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var result = await _service.Create(dto);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> List(int page = 1, int size = 10)
    {
        return Ok(await _service.List(page, size));
    }
}