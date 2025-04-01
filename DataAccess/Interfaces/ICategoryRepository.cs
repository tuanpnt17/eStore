using DataAccess.Entities;

namespace DataAccess.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetByIdAsync(int Id);
    Task CreateCategoryAsync(Category model);
    Task UpdateCategoryAsync(Category model);
    Task DeleteCategoryAsync(int Id);
    Task<bool> IsCategoryInUseAsync(int categoryId);
}
