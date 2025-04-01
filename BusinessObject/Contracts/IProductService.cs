using BusinessObject.Models.Product;
using DataAccess;
using DataAccess.Entities;

namespace BusinessObject.Contracts;

public interface IProductService
{
	Task<List<ProductViewDTO>> GetProductsAsync();
	Task<Product?> GetProductByIdAsync(int id);
	Task AddProductAsync(CreateProductDTO productDto);
	Task UpdateProductAsync(UpdateProductDTO productDto);
	Task DeleteProductAsync(int id);
	Task<PaginatedList<ProductViewDTO>> GetPagedProductsAsync(int pageIndex, int pageSize);
	Task<PaginatedList<ProductViewDTO>> SearchProductsAsync(string searchTerm, decimal maxPrice, int index, int pageSize);
}
