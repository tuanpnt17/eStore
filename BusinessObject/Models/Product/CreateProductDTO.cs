using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Product;

public class CreateProductDTO
{
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
}
