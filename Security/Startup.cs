using Security.Services;
using Security.Services.Implementation;
using Security.Services.Security;

namespace Security;

public static class Startup
{
    public static void ConfigureDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
    }
}