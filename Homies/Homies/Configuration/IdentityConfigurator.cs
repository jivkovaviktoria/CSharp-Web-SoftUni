using Homies.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Homies.Configuration;

public static class IdentityConfigurator
{
    public static void ConfigureIdentityServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<UserManager<ApplicationUser>>();
        builder.Services.AddScoped<SignInManager<ApplicationUser>>();
    }
}