using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess.Repositories;

public class OrderDetailRepository(AppDbContext context) : IOrderDetailRepository
{
    public async Task<List<OrderDetail>> GetOrderDetailByOrderIdAsync(int Id)
    {
        return await context.OrderDetails.Include(n => n.Product).Where(od => od.OrderId == Id).ToListAsync();
    }
}
