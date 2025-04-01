using DataAccess.Entities;
using System.Linq.Expressions;

namespace DataAccess.Interfaces;

public interface IProductRepository {
	IQueryable<Product> Entities { get; }
	Task<IList<Product>> GetAllAsync();
	Task<PaginatedList<Product>> GetPagging(IQueryable<Product> query, int index, int pageSize);
	Task<Product?> GetByIdAsync(int productId);
	Task InsertAsync(Product product);
	Task UpdateAsync(Product product);
	Task DeleteAsync(int productId);
	Task<PaginatedList<Product>> SearchAsync(IQueryable<Product> query, int index, int pageSize, Expression<Func<Product, bool>> filter);
}
