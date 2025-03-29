using static BusinessObject.Models.ProductDTO;

namespace BusinessObject.Contracts;

public interface IProductService {
    Task<IEnumerable<ProductListDto>> GetProductsListAsync();
    Task<ProductDetailsDto> GetProductDetailsAsync(int productId);
}
