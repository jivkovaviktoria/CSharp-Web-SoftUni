using Homies.Data.Contracts.Services;
using Homies.Services;

namespace Homies.Configuration;

public static class ServicesConfigurator
{
    public static void ConfigireServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventService, EventService>();
        builder.Services.AddScoped<ITypeService, TypeService>();
    }
}