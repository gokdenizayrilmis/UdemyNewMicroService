using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNewMicroService.Shared.Options;
using UdemyNewMicroService.Shared.Services;

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
                    ValidateLifetime = true
                };
            });

            services.AddAuthorization();

            return services;

        }
    }
}
