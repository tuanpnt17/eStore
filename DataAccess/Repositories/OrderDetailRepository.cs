using DataAccess.Data;

namespace DataAccess.Repositories;

public class OrderDetailRepository(AppDbContext context) : IOrderDetailRepository { }
