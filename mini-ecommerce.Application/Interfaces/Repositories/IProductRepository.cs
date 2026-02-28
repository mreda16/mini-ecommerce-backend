using mini_ecommerce.Domain.Entities;
namespace mini_ecommerce.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task Add(Product product);
    Task<Product?> Get(Guid id);
    Task<List<Product>> GetAll(int page, int pageSize);
    Task SaveChanges();
}