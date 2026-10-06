using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Security.Services;
using Security.Services.Authorization;
using Security.Services.Implementation;
using Security.Services.Security;

namespace Security;

public static class Startup
{
    public static void ConfigureDomainServices(this IServiceCollection services, string jwtSecret)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
        services.AddScoped<IAuthorizationService, JWTAuthorizationService>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "security-api",
                    ValidateAudience = true,
                    ValidAudience = "security-api",
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                };
            });
    }
    
}
