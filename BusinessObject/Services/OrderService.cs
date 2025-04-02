using BusinessObject.Contracts;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessObject.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        await _unitOfWork.OrderRepository.DeleteOrderAsync(orderId);
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _unitOfWork.OrderRepository.GetAllOrdersAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _unitOfWork.OrderRepository.GetOrderByIdAsync(orderId);
    }

    public async Task<List<Order>> GetOrderByUserIdAsync(int UserId)
    {
        return await _unitOfWork.OrderRepository.GetOrderByUserIdAsync(UserId);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        await _unitOfWork.OrderRepository.UpdateOrderAsync(order);
    }

    public async Task AddOrderAsync(Order order)
    {
        foreach (var orderDetail in order.OrderDetails)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(orderDetail.ProductId);
            if (product != null)
            {
                product.UnitsInStock -= orderDetail.Quantity;
                await _unitOfWork.ProductRepository.UpdateAsync(product);
            }
        }

        await _unitOfWork.OrderRepository.CreateOrderAsync(order);
        await _unitOfWork.Complete();
    }
}
