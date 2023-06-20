using Microsoft.AspNetCore.Identity;

namespace Homies.Data.Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<Event> Events { get; set; } = new List<Event>();
    public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
}