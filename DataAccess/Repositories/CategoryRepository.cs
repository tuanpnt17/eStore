using DataAccess.Data;

namespace DataAccess.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository { }
