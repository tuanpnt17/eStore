using DataAccess.Entities;

namespace DataAccess.Interfaces;

public interface ICategoryRepository {
    Task<Category?> GetByIdAsync(int id);
}
