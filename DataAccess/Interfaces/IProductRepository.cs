using DataAccess.Entities;

namespace DataAccess.Interfaces;

public interface IProductRepository {
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
}


