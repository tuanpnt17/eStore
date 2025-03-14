using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required, ForeignKey("Member")]
    public int MemberId { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    [Column(TypeName = "money")]
    public decimal? Freight { get; set; }

    // Navigation properties
    public virtual Member Member { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
