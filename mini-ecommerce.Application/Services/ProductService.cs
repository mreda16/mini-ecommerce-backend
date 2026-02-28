using mini_ecommerce.Application.Interfaces.Repositories;
using mini_ecommerce.Domain.Common;
using mini_ecommerce.Application.DTOs;
using mini_ecommerce.Application.Interfaces.Services;
using mini_ecommerce.Domain.Entities;
using mini_ecommerce.Application.Common;

namespace mini_ecommerce.Application.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> CreateAsync(CreateProductDTO request)
    {
        var result = Product.Create(request.Name, request.Price, request.Quantity);

        if (!result.IsSuccess)
            return Result<Guid>.Failure(result.Error);

        await _repository.AddAsync(result.Value);
        return Result<Guid>.Success(result.Value.Id);
    }

    public Task<PagedResult<ProductDto>> GetPagedAsync(int page, int size)
        => _repository.GetPagedAsync(page, size);
}