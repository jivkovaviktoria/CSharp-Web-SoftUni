using System.ComponentModel.DataAnnotations;

namespace Library.Models.Identity;

public class LoginRequest
{ 
    [Required]
    public string Email { get; set; } = null!;
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}