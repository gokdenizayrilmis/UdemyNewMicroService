using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using UdemyNewMicroService.Shared.Options;

namespace UdemyNewMicroService.Shared.Extensions
{
    public static class AuthenticationExt
    {
        public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services, IConfiguration configuration)
        {
            // Sign
            //Aud => payment.api vb.
            //Iss => http://localhost:8080/realms/udemyTenant
            //Token Life Time

            var identityOptions = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();
            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = identityOptions.Address;
                options.Audience = identityOptions.Audience;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    RoleClaimType = "roles",
                    NameClaimType = "preferred_username"
                };

            }).AddJwtBearer("ClientCredentialSchema", options =>
            {
                options.Authority = identityOptions.Address;
                options.Audience = identityOptions.Audience;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("x", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Email);
                    policy.RequireClaim("client_id");
                });
               
                options.AddPolicy("Password", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Email);
                });

                options.AddPolicy("ClientCredential", policy =>
                {
                    policy.AuthenticationSchemes.Add("ClientCredentialSchema");
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("client_id");
                });

            });

            return services;

        }
    }
}
