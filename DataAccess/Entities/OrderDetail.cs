using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class OrderDetail
{
    [Key, Column(Order = 0)]
    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [Key, Column(Order = 1)]
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [Required, Column(TypeName = "money")]
	public decimal UnitPrice { get; set; }

    [Required]
	public int Quantity { get; set; }

    public float Discount { get; set; }

    // Navigation properties
    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}
