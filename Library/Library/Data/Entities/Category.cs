using System.ComponentModel.DataAnnotations;
using Library.Data.Common;

namespace Library.Data.Entities;

public class Category : BaseEntity
{
    [Required]
    [MinLength(GlobalConstants.Category.NameMinLength), MaxLength(GlobalConstants.Category.NameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}