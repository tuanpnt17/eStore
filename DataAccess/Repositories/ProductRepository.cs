using DataAccess.Data;

namespace DataAccess.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository { }
