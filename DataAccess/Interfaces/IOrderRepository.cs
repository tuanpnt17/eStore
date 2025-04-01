using DataAccess.Entities;

namespace DataAccess.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int Id);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int Id);

    Task CreateOrderAsync(Order order);
}
