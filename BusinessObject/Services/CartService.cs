using System.Text.Json;
using BusinessObject.Contracts;
using BusinessObject.Models;
using StackExchange.Redis;

namespace BusinessObject.Services;

public class CartService : ICartService
{
    private readonly IDatabase _database;

    public CartService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<CartDTO?> GetCartAsync(string cartId)
    {
        var cart = await _database.StringGetAsync(cartId);
        if (string.IsNullOrEmpty(cart))
        {
            return null;
        }
        var result = JsonSerializer.Deserialize<CartDTO?>(cart.ToString());
        return result;
    }

    public async Task<CartDTO?> UpdateCartAsync(CartDTO cart)
    {
        var updatedCart = await _database.StringSetAsync(
            cart.Id,
            JsonSerializer.Serialize(cart),
            TimeSpan.FromDays(30)
        );
        if (!updatedCart)
            return null;
        return await GetCartAsync(cart.Id);
    }

    public Task<bool> DeleteCartAsync(string cartId)
    {
        return _database.KeyDeleteAsync(cartId);
    }

    public async Task<int?> GetCartItemsCount(string cartId)
    {
        var cartDto = await GetCartAsync(cartId);
        if (cartDto == null)
        {
            return null;
        }
        return cartDto.Items.Count();
    }
}
