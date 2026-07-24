using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using UdemyNewMicroService.Payment.Api;
using UdemyNewMicroService.Payment.Api.Features.Payment;
using UdemyNewMicroService.Payment.Api.Repositories;
using UdemyNewMicroService.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// 🟢 Swashbuckle ile Swagger üreticisini ve Bearer Token tanımını ekliyoruz
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "UdemyNewMicroService.Payment.Api", Version = "v1" });

    // 1. Bearer Token giriş kutusunu tanımlıyoruz
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Keycloak'tan aldığınız Access Token'ı buraya yapıştırın."
    });

    // 2. Bu güvenlik tanımını tüm Swagger endpoint'lerine uyguluyoruz
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddVersionningExt();
builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("payment-in-memory-db");
});

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();

app.AddPaymentGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
    // 🟢 Swashbuckle JSON ve UI katmanını aktif ediyoruz
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "UdemyNewMicroService.Payment.Api v1");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();