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
    public async Task<IActionResult> Create(CreateProductDTO request)
    {
        var result = await _service.CreateAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int page = 1, int size = 10)
    {
        var result = await _service.GetPagedAsync(page, size);
        return Ok(result);
    }
}