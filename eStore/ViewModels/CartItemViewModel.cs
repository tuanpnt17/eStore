using System.ComponentModel.DataAnnotations;

namespace eStore.ViewModels;

public class CartItemViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greager then 0.1 dollars")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    [Required]
    public double Discount { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;
}
