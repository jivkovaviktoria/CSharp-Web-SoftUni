using System.ComponentModel.DataAnnotations;

namespace Library.Models.Identity;

public class RegisterRequest
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;
}