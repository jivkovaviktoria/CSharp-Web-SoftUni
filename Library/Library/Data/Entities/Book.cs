using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library.Data.Common;

namespace Library.Data.Entities;

public class Book : BaseEntity
{
    [Required]
    [MinLength(GlobalConstants.Book.TitleMinLength), MaxLength(GlobalConstants.Book.TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(GlobalConstants.Book.AuthorMinLegnth), MaxLength(GlobalConstants.Book.AuthorMaxLegnth)]
    public string Author { get; set; } = null!;

    [Required]
    [MinLength(GlobalConstants.Book.DescriptionMinLength), MaxLength(GlobalConstants.Book.DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;
    
    [Required]
    public decimal Rating { get; set; }
    
    [Required]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public ICollection<ApplicationUserBook> UsersBooks { get; set; } = new List<ApplicationUserBook>();
}