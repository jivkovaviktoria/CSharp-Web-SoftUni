using Library.Data.Entities;
using Library.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        this._signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        if (this.UserIsLogedIn()) return RedirectToAction("All", "Book");

        var registerModel = new RegisterRequest();
        return View(registerModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid) return View(registerRequest);

        var user = new ApplicationUser()
        {
            Email = registerRequest.Email
        };

        var result = await this._userManager.CreateAsync(user, registerRequest.Password);

        if (result.Succeeded) return RedirectToAction("Login");
        
        foreach(var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
        return View(registerRequest);
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        if (this.UserIsLogedIn()) return RedirectToAction("All", "Book");

        var loginModel = new LoginRequest();
        return View(loginModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        if (!ModelState.IsValid) return View(loginRequest);

        var user = await this._userManager.FindByEmailAsync(loginRequest.Email);

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Bad credentials");
            return View(new LoginRequest());
        }
        var result = await this._signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);

        if (result.Succeeded) return RedirectToAction("All", "Book");
        
        ModelState.AddModelError(string.Empty, "Bad credentials");
        return View(loginRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await this._signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    private bool UserIsLogedIn() => User.Identity?.IsAuthenticated ?? false;
}