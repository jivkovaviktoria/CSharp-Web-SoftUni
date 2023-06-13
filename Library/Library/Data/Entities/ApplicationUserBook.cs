using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities;

public class ApplicationUserBook
{
    [Required]
    [ForeignKey(nameof(Collector))]
    public string CollectorId { get; set; } = null!;

    public ApplicationUser Collector { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Book))]
    public int BookId { get; set; }
    public Book Book { get; set; }
}