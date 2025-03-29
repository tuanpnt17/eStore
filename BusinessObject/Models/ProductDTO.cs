namespace BusinessObject.Models;

public class ProductDTO
{
    public class ProductListDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string Weight { get; set; } = string.Empty;
    }
    public class ProductDetailsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string Weight { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
