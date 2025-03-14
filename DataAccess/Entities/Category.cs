namespace DataAccess.Entities;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required, StringLength(50)]
    public required string CategoryName { get; set; }

    [StringLength(255)]
    public string? CategoryDescription { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
