using mini_ecommerce.Application.Interfaces.Repositories;
using mini_ecommerce.Domain.Common;
using mini_ecommerce.Application.DTOs;
using mini_ecommerce.Application.Interfaces.Services;
using mini_ecommerce.Domain.Entities;

namespace mini_ecommerce.Application.Services;

public class ProductService: IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<ProductDto>> Create(CreateProductDto dto)
    {
        var productResult = Product.Create(dto.Name, dto.Price, dto.Quantity);

        if (!productResult.IsSuccess)
            return Result<ProductDto>.Failure(productResult.Error);

        await _repo.Add(productResult.Value);
        await _repo.SaveChanges();

        var p = productResult.Value;

        return Result<ProductDto>.Success(
            new ProductDto(p.Id, p.Name, p.Price, p.Quantity));
    }

    public async Task<List<ProductDto>> List(int page, int pageSize)
    {
        var products = await _repo.GetAll(page, pageSize);

        return products.Select(x =>
            new ProductDto(x.Id, x.Name, x.Price, x.Quantity)).ToList();
    }
}