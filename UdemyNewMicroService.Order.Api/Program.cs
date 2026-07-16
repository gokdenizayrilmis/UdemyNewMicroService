using Microsoft.EntityFrameworkCore;
using UdemyNewMicroService.Order.Application.Contracts.Repositories;
using UdemyNewMicroService.Order.Persistence;
using UdemyNewMicroService.Order.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => 
{ 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();

