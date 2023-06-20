using System.ComponentModel.DataAnnotations;
using Homies.Data.Common;
using Homies.Data.Entities;

namespace Homies.Services.Models.Event;

public class EventInputModel
{
    [Required]
    [StringLength(GlobalConstants.EventConstants.NameMaxLength, MinimumLength = GlobalConstants.EventConstants.NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(GlobalConstants.EventConstants.DescriptionMaxLength, MinimumLength = GlobalConstants.EventConstants.DescriptionMinLength)]
    public string Description { get; set; } = null!;]
}


    [Required]
    public string OrganiserId { get; set; } = null!;

    public ApplicationUser Organiser { get; set; } = null!;

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = GlobalConstants.EventConstants.DateFormat)]
    public DateTime CreatedOn { get; set; }
    
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = GlobalConstants.EventConstants.DateFormat)]
    public DateTime Start { get; set; }
    
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = GlobalConstants.EventConstants.DateFormat)]
    public DateTime End { get; set; }

    [Required]
    public int TypeId { get; set; }
    
    public Type Type { get; set; } = null!;

    public ICollection<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
}
