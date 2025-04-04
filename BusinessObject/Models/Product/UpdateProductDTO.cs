﻿using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Product;

public class UpdateProductDTO
{
	public int ProductId { get; set; }
	public int? CategoryId { get; set; }
	[StringLength(40, ErrorMessage = "Độ dài tên sản phẩm không quá 40 kí tự.")]
	public string? ProductName { get; set; }
	[StringLength(20, ErrorMessage = "Độ dài mô tả cân nặng không quá 20 kí tự.")]
	public string? Weight { get; set; }
	[Range(1000, double.MaxValue, ErrorMessage = "Giá không thể thấp hơn 1000.")]
	public decimal? UnitPrice { get; set; }
	[Range(0, int.MaxValue, ErrorMessage = "Tồn kho không thể âm.")]
	public int? UnitsInStock { get; set; }
}
