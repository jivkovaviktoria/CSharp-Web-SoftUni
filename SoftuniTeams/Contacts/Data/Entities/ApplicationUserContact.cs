using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace Contacts.Data.Entities;

public class ApplicationUserContact
{
    [Required]
    [ForeignKey(nameof(Entities.ApplicationUser))]
    public string ApplicationUserId { get; set; } = null!;
    
    public ApplicationUser ApplicationUser { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Contact))]
    public int ContactId { get; set; }

    public Contact Contact { get; set; } = null!;
}
