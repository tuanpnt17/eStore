using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class CartDTO
{
    public CartDTO() { }

    public CartDTO(string id)
    {
        Id = id;
        Items = [];
    }

    [Required]
    public required string Id { get; set; }

    public List<CartItemDTO> Items { get; set; } = [];
}
