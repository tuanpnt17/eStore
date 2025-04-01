using BusinessObject.Models;
using DataAccess.Entities;

namespace BusinessObject.Contracts;

public interface ICategoryService 
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task DeleteCategoryAsync(int cateId);
    Task CreateCategoryAsync(CategoryDTO category);
    Task UpdateCategoryAsync(Category model);
}
