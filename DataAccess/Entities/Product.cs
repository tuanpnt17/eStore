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
    public decimal UnitPrice { get; set; }

    [Required]
    public int UnitsInStock { get; set; }

    // Navigation property for related OrderDetails
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
