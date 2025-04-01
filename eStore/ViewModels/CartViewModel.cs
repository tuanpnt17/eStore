using System.ComponentModel.DataAnnotations;

namespace eStore.ViewModels;

public class CartViewModel
{
    [Required]
    public required string Id { get; set; }

    [Required]
    public List<CartItemViewModel> Items { get; set; } = [];
}
