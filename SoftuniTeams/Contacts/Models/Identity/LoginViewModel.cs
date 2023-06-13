﻿using System.ComponentModel.DataAnnotations;

namespace Contacts.Models.Identity;

public class LoginViewModel
{
    [Required]
    public string UserName { get; set; } = null!;
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}