using Microsoft.EntityFrameworkCore;
using mini_ecommerce.Infrastructure.Persistence;
using mini_ecommerce.Application.Interfaces.Repositories;
using mini_ecommerce.Domain.Entities;

namespace mini_ecommerce.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public Task<Order?> GetByIdAsync(Guid id)
    {
        return _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}