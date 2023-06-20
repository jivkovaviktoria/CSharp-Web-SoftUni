using System.ComponentModel.DataAnnotations;
using Homies.Data.Common;

namespace Homies.Data.Entities;

public class Type : BaseEntity
{
    [StringLength(GlobalConstants.TypeConstants.NameMaxLength, MinimumLength = GlobalConstants.TypeConstants.NameMinLength)]
    public string Name { get; set; } = null!;

    public ICollection<Event> Events { get; set; } = new List<Event>();
}