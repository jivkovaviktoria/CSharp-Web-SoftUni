using Contacts.Data.Entities;
using Contacts.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (this.UserIsAutheticated()) return RedirectToAction("All", "Contact");
        
        var registerViewModel = new RegisterViewModel();
        return View(registerViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);

        ApplicationUser user = new ApplicationUser()
        {
            UserName = registerViewModel.UserName,
            Email = registerViewModel.Email
        };

        var result = await this._userManager.CreateAsync(user, registerViewModel.Password);

        if (result.Succeeded) return RedirectToAction("Login");
        
        foreach(var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
        return View(registerViewModel);
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (this.UserIsAutheticated()) return RedirectToAction("All", "Contact");
        
        var loginViewModel = new LoginViewModel();
        return View(loginViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) return View(loginViewModel);

        var user = await this._userManager.FindByNameAsync(loginViewModel.UserName);

        if (user is not null)
        {
            var result = await this._signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
            if (result.Succeeded) return RedirectToAction("All", "Contact");
        }
        
        ModelState.AddModelError(string.Empty, "Bad credentials");
        return View(loginViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await this._signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    private bool UserIsAutheticated() => User?.Identity?.IsAuthenticated ?? false;
}