using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Homies.Data.Entities;

public class EventParticipant
{
    [Required]
    public string HelperId { get; set; } = null!;

    public ApplicationUser Helper { get; set; } = null!;

    [Required]
    public int EventId { get; set; }
    
    public Event Event { get; set; } = null!;
}
