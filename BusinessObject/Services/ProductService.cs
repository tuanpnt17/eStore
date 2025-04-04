﻿using System.Linq.Expressions;
using AutoMapper;
using BusinessObject.Contracts;
using BusinessObject.Models.Product;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using static BusinessObject.Models.ProductDTO;

namespace BusinessObject.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ProductViewDTO>> GetProductsAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();
        var result = _mapper.Map<List<ProductViewDTO>>(products);
        return result;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
        return product;
    }

    public async Task AddProductAsync(CreateProductDTO productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        await _unitOfWork.ProductRepository.InsertAsync(product);
        await _unitOfWork.Complete();
    }

    public async Task UpdateProductAsync(UpdateProductDTO productDto)
    {
        var product =
            await GetProductByIdAsync(productDto.ProductId)
            ?? throw new Exception($"Product with ID {productDto.ProductId} does not exist.");
        _mapper.Map(productDto, product);
        await _unitOfWork.ProductRepository.UpdateAsync(product);
        await _unitOfWork.Complete();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product =
            await GetProductByIdAsync(id)
            ?? throw new Exception($"Sản phẩm với ID {id} không tồn tại.");
        if (product.OrderDetails != null && product.OrderDetails.Any())
        {
            throw new InvalidOperationException(
                $"Không thể xóa sản phẩm với ID {id} vì nó đã có chi tiết đơn hàng liên quan."
            );
        }
        await _unitOfWork.ProductRepository.DeleteAsync(id);
        await _unitOfWork.Complete();
    }

    public async Task<PaginatedList<ProductViewDTO>> GetPagedProductsAsync(
        int pageIndex,
        int pageSize
    )
    {
        var query = _unitOfWork.ProductRepository.Entities;
        var paginatedProducts = await _unitOfWork.ProductRepository.GetPagging(
            query,
            pageIndex,
            pageSize
        );
        return new PaginatedList<ProductViewDTO>(
            _mapper.Map<List<ProductViewDTO>>(paginatedProducts.Items),
            paginatedProducts.TotalItems,
            pageIndex,
            pageSize
        );
    }

    public async Task<PaginatedList<ProductViewDTO>> SearchProductsAsync(
        string searchTerm,
        decimal maxPrice,
        int index,
        int pageSize
    )
    {
        var query = _unitOfWork.ProductRepository.Entities;
        Expression<Func<Product, bool>> filter = p =>
            (string.IsNullOrWhiteSpace(searchTerm) || p.ProductName.Contains(searchTerm))
            && p.UnitPrice <= maxPrice;

        var paginatedProducts = await _unitOfWork.ProductRepository.SearchAsync(
            query,
            index,
            pageSize,
            filter
        );
        return new PaginatedList<ProductViewDTO>(
            _mapper.Map<List<ProductViewDTO>>(paginatedProducts.Items),
            paginatedProducts.TotalItems,
            index,
            pageSize
        );
    }

    public async Task<IEnumerable<ProductListDto>> GetProductsListAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();

        return products.Select(p => new ProductListDto
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            UnitPrice = p.UnitPrice,
            Weight = p.Weight,
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
            UnitsInStock = product.UnitsInStock,
        };
    }
}
