using DataAccess.Data;

namespace DataAccess.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository { }
