using System.Linq.Expressions;
using DataAccess.Entities;

namespace DataAccess.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    IQueryable<Product> Entities { get; }
    Task<PaginatedList<Product>> GetPagging(IQueryable<Product> query, int index, int pageSize);
    Task<Product?> GetByIdAsync(int productId);
    Task InsertAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int productId);
    Task<PaginatedList<Product>> SearchAsync(
        IQueryable<Product> query,
        int index,
        int pageSize,
        Expression<Func<Product, bool>> filter
    );
}
