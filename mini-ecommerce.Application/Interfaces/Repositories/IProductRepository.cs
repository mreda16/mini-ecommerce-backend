using mini_ecommerce.Application.Common;
using mini_ecommerce.Application.DTOs;
using mini_ecommerce.Domain.Entities;
namespace mini_ecommerce.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);

    Task<PagedResult<ProductDto>> GetPagedAsync(int pageNumber, int pageSize);
}