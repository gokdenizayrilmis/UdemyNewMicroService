using UdemyNewMicroService.Basket.Api;
using UdemyNewMicroService.Basket.Api.Features.Baskets;
using UdemyNewMicroService.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddScoped<BasketService>();
builder.Services.AddVersionningExt();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // .NET 10'un /openapi/v1.json endpoint'ini aktif eder
    app.MapOpenApi();

    // Swagger UI'a yeni .json yolunu bildirir
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "UdemyNewMicroService.Catalog.Api v1");
    });
}

app.AddBasketGroupEndpointExt(app.AddVersionSetExt());

app.Run();
