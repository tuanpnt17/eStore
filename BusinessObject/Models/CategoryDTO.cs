using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class CategoryDTO 
{
    public required string CategoryName { get; set; } = string.Empty;
    public string? CategoryDescription { get; set; }
    public CategoryDTO() { }
}
