using BusinessObject.Models;

namespace BusinessObject.Contracts;

public interface ICartService
{
    Task<CartDTO?> GetCartAsync(string cartId);
    Task<CartDTO?> UpdateCartAsync(CartDTO cart);
    Task<bool> DeleteCartAsync(string cartId);
    Task<int?> GetCartItemsCount(string cartId);
}
