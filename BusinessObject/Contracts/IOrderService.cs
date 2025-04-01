using BusinessObject.Models;
using DataAccess.Entities;

namespace BusinessObject.Contracts;

public interface IOrderService 
{
    Task<List<Order>> GetAllOrdersAsync();
    Task DeleteOrderAsync(int orderId);
    Task<Order> GetOrderByIdAsync(int orderId);
    Task UpdateOrderAsync(Order order);
    Task<List<Order>> GetOrderByUserIdAsync(int UserId);
}
