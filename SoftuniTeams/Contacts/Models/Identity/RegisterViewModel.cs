using System.ComponentModel.DataAnnotations;
using Contacts.Data.Common;

namespace Contacts.Models.Identity;

public class RegisterViewModel
{
    [Required]
    [StringLength(GlobalConstants.UserConstants.UserNameMaxLength, MinimumLength = GlobalConstants.UserConstants.UserNameMinLength)]
    public string UserName { get; set; } = null!;

    [Required]
    [StringLength(GlobalConstants.UserConstants.EmailMaxLength, MinimumLength = GlobalConstants.UserConstants.EmailMinLength)]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(GlobalConstants.UserConstants.PasswordMaxLength, MinimumLength = GlobalConstants.UserConstants.PasswordMinLength)]
    public string Password { get; set; } = null!;

    [Required] 
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}