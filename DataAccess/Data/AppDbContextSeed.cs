using System.Text.Json;
using DataAccess.Entities;
using Microsoft.Extensions.Logging;

namespace DataAccess.Data;

public static class AppDbContextSeed
{
    public static string SeedDataPath { get; set; } =
        Path.Combine(Directory.GetCurrentDirectory(), "../DataAccess/Data/SeedData");

    public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Categories.Any())
            {
                var categoryData = await File.ReadAllTextAsync(
                    Path.Combine(SeedDataPath, "categories.json")
                );
                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);
                if (categories != null)
                {
                    foreach (var item in categories)
                    {
                        context.Categories.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Products.Any())
            {
                var productData = await File.ReadAllTextAsync(
                    Path.Combine(SeedDataPath, "products.json")
                );
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products != null)
                {
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Members.Any())
            {
                var memberData = await File.ReadAllTextAsync(
                    Path.Combine(SeedDataPath, "members.json")
                );
                var members = JsonSerializer.Deserialize<List<Member>>(memberData);
                if (members != null)
                {
                    foreach (var item in members)
                    {
                        context.Members.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Orders.Any())
            {
                var orderData = await File.ReadAllTextAsync(
                    Path.Combine(SeedDataPath, "orders.json")
                );
                var orders = JsonSerializer.Deserialize<List<Order>>(orderData);
                if (orders != null)
                {
                    foreach (var item in orders)
                    {
                        context.Orders.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (!context.OrderDetails.Any())
            {
                var orderDetailData = await File.ReadAllTextAsync(
                    Path.Combine(SeedDataPath, "order_details.json")
                );
                var orderDetails = JsonSerializer.Deserialize<List<OrderDetail>>(orderDetailData);
                if (orderDetails != null)
                {
                    foreach (var item in orderDetails)
                    {
                        context.OrderDetails.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<AppDbContext>();
            logger.LogError(ex.Message);
        }
    }
}
