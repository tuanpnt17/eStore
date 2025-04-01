using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task DeleteOrderAsync(int Id)
    {
        var model = await context.Orders.FirstOrDefaultAsync(n => n.OrderId == Id);
        context.Orders.Remove(model);
        await context.SaveChangesAsync();
    }

    public async Task CreateOrderAsync(Order order)
    {
        await context.Orders.AddAsync(order);
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await context
            .Orders.Include(n => n.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int Id)
    {
        return await context
            .Orders.Include(n => n.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(n => n.OrderId == Id);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();
    }
}
