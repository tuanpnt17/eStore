using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task CreateCategoryAsync(Category model)
    {
        await context.Categories.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int Id)
    {
        var model = context.Categories.FirstOrDefault(c => c.CategoryId == Id);
        context.Remove(model);  
        await context.SaveChangesAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task UpdateCategoryAsync(Category model)
    {
        context.Categories.Update(model);
        await context.SaveChangesAsync();
    }
    public async Task<bool> IsCategoryInUseAsync(int categoryId)
    {
        // Check if any product references this category
        return await context.Products.AnyAsync(p => p.CategoryId == categoryId);
    }
}
