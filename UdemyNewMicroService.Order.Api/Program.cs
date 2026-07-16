using Microsoft.EntityFrameworkCore;
using UdemyNewMicroService.Order.Api.Endpoints.Orders;
using UdemyNewMicroService.Order.Application.Contracts.Repositories;
using UdemyNewMicroService.Order.Application.Contracts.UnitOfWorks;
using UdemyNewMicroService.Order.Persistence;
using UdemyNewMicroService.Order.Persistence.Repositories;
using UdemyNewMicroService.Order.Persistence.UnitOfWork;
using UdemyNewMicroService.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options => 
{   options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")); });

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddVersionningExt();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();






var app = builder.Build();

app.AddOrderGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
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

app.Run();

