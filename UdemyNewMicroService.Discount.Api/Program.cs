using UdemyNewMicroService.Discount.Api;
using UdemyNewMicroService.Discount.Api.Features.Discounts;
using UdemyNewMicroService.Discount.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
builder.Services.AddVersionningExt();





var app = builder.Build();

app.AddDiscountGroupEndpointExt(app.AddVersionSetExt());


if (app.Environment.IsDevelopment())
{
    // .NET 10'un /openapi/v1.json endpoint'ini aktif eder
    app.MapOpenApi();

    // Swagger UI'a yeni .json yolunu bildirir
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "UdemyNewMicroService.Discount.Api v1");
    });
}

app.Run();

