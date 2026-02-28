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

    public async Task Add(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<Order?> Get(Guid id)
    {
        return await _context.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}