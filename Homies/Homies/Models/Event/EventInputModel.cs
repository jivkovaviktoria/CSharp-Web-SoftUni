using System.ComponentModel.DataAnnotations;
using Homies.Data.Common;
using Homies.Models.Type;

namespace Homies.Models.Event;

public class EventInputModel
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(GlobalConstants.EventConstants.NameMaxLength, MinimumLength = GlobalConstants.EventConstants.NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(GlobalConstants.EventConstants.DescriptionMaxLength, MinimumLength = GlobalConstants.EventConstants.DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = GlobalConstants.EventConstants.DateFormat)]
    public string Start { get; set; } = null!;

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = GlobalConstants.EventConstants.DateFormat)]
    public string End { get; set; } = null!;

    [Required]
    public int TypeId { get; set; }

    public IEnumerable<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
}