using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Product;

public class CreateProductDTO
{
	public int CategoryId { get; set; }
	[Required(ErrorMessage = "Tên sản phẩm là bắt buộc."), StringLength(40, ErrorMessage = "Độ dài tên sản phẩm không quá 40 kí tự.")]
	public required string ProductName { get; set; }
	[Required(ErrorMessage = "Cân nặng sản phẩm là bắt buộc."), StringLength(20, ErrorMessage = "Độ dài mô tả cân nặng không quá 20 kí tự.")]
	public required string Weight { get; set; }
	[Required, Column(TypeName = "money")]
	[Range(1000, double.MaxValue, ErrorMessage = "Giá không thể thấp hơn 1000.")]
	public decimal UnitPrice { get; set; }
	[Required]
	[Range(0, int.MaxValue, ErrorMessage = "Tồn kho không thể âm.")]
	public int UnitsInStock { get; set; }
}
