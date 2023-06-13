using System.ComponentModel.DataAnnotations;
using Library.Data.Common;
using Library.Models.Entities.Category;

namespace Library.Models.Entities.Book;

public class BookInputModel
{
    [Required]
    public string ImageUrl { get; set; } = null!;
        
    [Required]
    [MinLength(GlobalConstants.Book.TitleMinLength), MaxLength(GlobalConstants.Book.TitleMaxLength)]
    public string Title { get; set; } = null!;
        
    [Required]
    [MinLength(GlobalConstants.Book.AuthorMinLegnth), MaxLength(GlobalConstants.Book.AuthorMaxLegnth)]
    public string Author { get; set; } = null!;
    
    [Required]
    [MinLength(GlobalConstants.Book.DescriptionMinLength), MaxLength(GlobalConstants.Book.DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Range(typeof(decimal), "0.0", "10.0")]
    public decimal Rating { get; set; }

    public int CategoryId { get; set; }

    public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
}