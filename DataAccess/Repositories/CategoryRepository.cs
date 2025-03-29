using DataAccess.Data;
using DataAccess.Entities;

namespace DataAccess.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository {
    private readonly AppDbContext _context = context;

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }
}
