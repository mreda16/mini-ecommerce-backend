using mini_ecommerce.Domain.Entities;
namespace mini_ecommerce.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task Add(Order order);
    Task<Order?> Get(Guid id);
    Task SaveChanges();
}