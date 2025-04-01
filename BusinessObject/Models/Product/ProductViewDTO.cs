using DataAccess.Entities;
using System.Text.Json.Serialization;

namespace BusinessObject.Models.Product;

public class ProductViewDTO
{
	public int ProductId { get; set; }
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public required Category Category { get; set; }
	public required string ProductName { get; set; }
	public required string Weight { get; set; }
	public decimal UnitPrice { get; set; }
	public int UnitsInStock { get; set; }
}
