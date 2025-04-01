using System.Linq.Expressions;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<Product> _dbSet;

    public ProductRepository(AppDbContext dbContext)
    {
        _context = dbContext;
        _dbSet = _context.Set<Product>();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public IQueryable<Product> Entities => _context.Set<Product>();

    public async Task<Product?> GetByIdAsync(int productId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.OrderDetails)
            .FirstOrDefaultAsync(p => p.ProductId == productId);
    }

    public async Task<PaginatedList<Product>> GetPagging(
        IQueryable<Product> query,
        int index,
        int pageSize
    )
    {
        query = query.Include(p => p.Category).AsNoTracking();
        int count = await query.CountAsync();
        IReadOnlyCollection<Product> items = await query
		    .OrderByDescending(p => p.ProductId)
			.Skip((index - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PaginatedList<Product>(items, count, index, pageSize);
    }

    public async Task InsertAsync(Product product)
    {
        await _dbSet.AddAsync(product);
    }

    public Task UpdateAsync(Product product)
    {
        return Task.FromResult(_dbSet.Update(product));
    }

    public async Task DeleteAsync(int productId)
    {
        Product product =
            await _dbSet.FindAsync(productId) ?? throw new Exception("Entity not found.");
        _dbSet.Remove(product);
    }

    public async Task<PaginatedList<Product>> SearchAsync(
        IQueryable<Product> query,
        int index,
        int pageSize,
        Expression<Func<Product, bool>> filter
    )
    {
        query = query.Include(p => p.Category).AsNoTracking();
        int count = query.Where(filter).Count();
        IReadOnlyCollection<Product> items = await query
            .Where(filter)
			.OrderByDescending(p => p.ProductId)
			.Skip((index - 1) * pageSize)
            .Take(pageSize)
			.ToListAsync();
        return new PaginatedList<Product>(items, count, index, pageSize);
    }
}
