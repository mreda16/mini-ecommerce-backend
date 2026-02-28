using Microsoft.EntityFrameworkCore;
using mini_ecommerce.Domain.Entities;

namespace mini_ecommerce.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}