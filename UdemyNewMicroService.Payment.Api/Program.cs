using Microsoft.EntityFrameworkCore;
using UdemyNewMicroService.Payment.Api;
using UdemyNewMicroService.Payment.Api.Features.Payment;
using UdemyNewMicroService.Payment.Api.Repositories;
using UdemyNewMicroService.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
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
    // .NET 10'un /openapi/v1.json endpoint'ini aktif eder
    app.MapOpenApi();

    // Swagger UI'a yeni .json yolunu bildirir
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "UdemyNewMicroService.Order.Api v1");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
