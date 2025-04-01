using BusinessObject.Contracts;
using BusinessObject.Models;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessObject.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

    public Task<List<Category>> GetAllCategoriesAsync()
    {
        return _unitOfWork.CategoryRepository.GetAllCategoriesAsync();
    }

    public async Task UpdateCategoryAsync(Category model)
    {
       await _unitOfWork.CategoryRepository.UpdateCategoryAsync(model);
    }
}
