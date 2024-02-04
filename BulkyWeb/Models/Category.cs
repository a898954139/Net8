using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [DisplayName("Category Name")] 
    [MaxLength(30, ErrorMessage = "Name must be less than 30 characters")]
    public required string Name { get; set; }
    [DisplayName("Display Order")]
    [Range(1,100, ErrorMessage = "Display Order must be within the 1-100")]
    public int DisplayOrder { get; set; }
}