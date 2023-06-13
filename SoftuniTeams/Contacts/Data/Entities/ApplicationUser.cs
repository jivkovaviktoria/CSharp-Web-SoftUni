using System.ComponentModel.DataAnnotations;
using Contacts.Data.Common;
using Microsoft.AspNetCore.Identity;

namespace Contacts.Data.Entities;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(GlobalConstants.UserConstants.UserNameMaxLength, MinimumLength = GlobalConstants.UserConstants.UserNameMinLength)]
    public override string UserName { get => base.UserName; set => base.UserName = value; }

    [Required]
    [StringLength(GlobalConstants.UserConstants.EmailMaxLength, MinimumLength = GlobalConstants.UserConstants.EmailMinLength)]
    public override string Email { get => base.Email; set => base.Email = value; }
}