using mini_ecommerce.Domain.Entities;
namespace mini_ecommerce.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order);

    Task<Order?> GetByIdAsync(Guid id);
}