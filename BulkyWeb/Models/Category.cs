using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models;

public class Category
{
    [Key]
    public int Key { get; set; }
    [Required]
    public string? Name { get; set; }
    public int DisplayOrder { get; set; }
}