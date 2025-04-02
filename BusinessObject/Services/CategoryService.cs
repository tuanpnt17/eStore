using BusinessObject.Contracts;
using BusinessObject.Models;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return (await _unitOfWork.CategoryRepository.GetByIdAsync(id))
            ?? throw new InvalidOperationException($"Danh mục với ID {id} không tồn tại!");
    }

    public async Task CreateCategoryAsync(CategoryDTO category)
    {
        var model = new Category()
        {
            CategoryName = category.CategoryName,
            CategoryDescription = category.CategoryDescription
        };
        await _unitOfWork.CategoryRepository.CreateCategoryAsync(model);
    }

    public async Task DeleteCategoryAsync(int cateId)
    {
        await _unitOfWork.CategoryRepository.DeleteCategoryAsync(cateId);
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _unitOfWork.CategoryRepository.GetAllCategoriesAsync();
    }

    public async Task UpdateCategoryAsync(Category model)
    {
       await _unitOfWork.CategoryRepository.UpdateCategoryAsync(model);
       await _unitOfWork.Complete();
    }
    public async Task<bool> IsCategoryInUseAsync(int categoryId)
    {
        // Check if any product references this category
        return await _unitOfWork.CategoryRepository.IsCategoryInUseAsync(categoryId);
    }
}
