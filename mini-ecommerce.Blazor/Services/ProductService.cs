using mini-ecommerce.Blazor.Models;

namespace mini-ecommerce.Blazor.Services;

public class ProductService
{
    private readonly ApiClient _api;

    public ProductService(ApiClient api)
    {
        _api = api;
    }

    public async Task<List<ProductDto>> GetProducts()
    {
        var result = await _api.Get<List<ProductDto>>("api/products");

        return result ?? new();
    }

    public async Task<ProductDto?> CreateProduct(CreateProductDto dto)
    {
        return await _api.Post<CreateProductDto, ProductDto>("api/products", dto);
    }
}