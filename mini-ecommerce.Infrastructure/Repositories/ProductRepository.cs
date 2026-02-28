using Microsoft.EntityFrameworkCore;
using mini_ecommerce.Infrastructure.Persistence;
using mini_ecommerce.Application.Interfaces.Repositories;
using mini_ecommerce.Domain.Entities;
using mini_ecommerce.Application.DTOs;
using mini_ecommerce.Application.Common;

namespace mini_ecommerce.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public Task<Product?> GetByIdAsync(Guid id)
        => _context.Products.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<PagedResult<ProductDto>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var query = _context.Products.AsNoTracking();

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ProductDto(
                x.Id,
                x.Name,
                x.Price,
                x.Quantity))
            .ToListAsync();

        return new PagedResult<ProductDto>(items, total, pageNumber, pageSize);
    }
    
    public async Task<ProductDto?> GetProductDetailsByIdAsync(Guid id)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new ProductDto(
                x.Id,
                x.Name,
                x.Price,
                x.Quantity))
            .FirstOrDefaultAsync();
    }
}