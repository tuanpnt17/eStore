using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required, ForeignKey("Category")]
    public int CategoryId { get; set; }

    [Required, StringLength(40)]
    public required string ProductName { get; set; }

    [Required, StringLength(20)]
    public required string Weight { get; set; }
	[Required, Column(TypeName = "money")]
	[Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
	public decimal UnitPrice { get; set; }

    [Required]
	[Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
	public int UnitsInStock { get; set; }

    // Navigation property for related OrderDetails
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public virtual required Category Category { get; set; }
}
