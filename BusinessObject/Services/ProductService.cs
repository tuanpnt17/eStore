using BusinessObject.Contracts;
using DataAccess.Interfaces;
using static BusinessObject.Models.ProductDTO;

namespace BusinessObject.Services;

public class ProductService : IProductService {
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductListDto>> GetProductsListAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();

        return products.Select(p => new ProductListDto
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            UnitPrice = p.UnitPrice,
            Weight = p.Weight
        });
    }

    public async Task<ProductDetailsDto> GetProductDetailsAsync(int productId)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {productId} not found");
        }

        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(product.CategoryId);

        return new ProductDetailsDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            UnitPrice = product.UnitPrice,
            Weight = product.Weight,
            CategoryName = category?.CategoryName ?? "Unknown Category",
            UnitsInStock = product.UnitsInStock
        };
    }
}
