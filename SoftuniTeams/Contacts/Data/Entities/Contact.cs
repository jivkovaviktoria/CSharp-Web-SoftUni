using System.ComponentModel.DataAnnotations;
using Contacts.Data.Common;

namespace Contacts.Data.Entities;

public class Contact : BaseEntity
{
    [Required]
    [StringLength(GlobalConstants.ContactConstants.FirstNameMaxLength, MinimumLength = GlobalConstants.ContactConstants.FirstNameMinLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(GlobalConstants.ContactConstants.LastNameMaxLength, MinimumLength = GlobalConstants.ContactConstants.LastNameMinLength)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(GlobalConstants.ContactConstants.EmailMaxLength, MinimumLength = GlobalConstants.ContactConstants.EmailMinLength)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(GlobalConstants.ContactConstants.PhoneNumberMaxLength, MinimumLength = GlobalConstants.ContactConstants.PhoneNumberMinLength)]
    [RegularExpression(GlobalConstants.ContactConstants.PhoneNumberFormat)]
    public string PhoneNumber { get; set; } = null!;
    
    public string Address { get; set; }

    [Required] 
    [RegularExpression(GlobalConstants.ContactConstants.WebsiteFormat)]
    public string Website { get; set; } = null!;

    public ICollection<ApplicationUserContact> ApplicationUsersContacts { get; set; } = new List<ApplicationUserContact>();
}
