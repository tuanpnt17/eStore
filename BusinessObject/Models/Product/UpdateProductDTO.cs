using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Product;

public class UpdateProductDTO
{
	public int ProductId { get; set; }
	public int? CategoryId { get; set; }
	[StringLength(40)]
	public string? ProductName { get; set; }
	[StringLength(20)]
	public string? Weight { get; set; }
	[Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
	public decimal? UnitPrice { get; set; }
	[Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
	public int? UnitsInStock { get; set; }
}
