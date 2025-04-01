using DataAccess.Entities;

namespace DataAccess.Interfaces;

public interface IOrderDetailRepository 
{
    Task<List<OrderDetail>> GetOrderDetailByOrderIdAsync(int Id);

}
