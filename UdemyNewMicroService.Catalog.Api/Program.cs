using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UdemyNewMicroService.Catalog.Api;
using UdemyNewMicroService.Catalog.Api.Features.Categories;
using UdemyNewMicroService.Catalog.Api.Features.Categories.Create;
using UdemyNewMicroService.Catalog.Api.Features.Courses;
using UdemyNewMicroService.Catalog.Api.Options;
using UdemyNewMicroService.Catalog.Api.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
builder.Services.AddVersionningExt();


var app = builder.Build();

app.AddSeedDataExt().ContinueWith(x=>
{
    if (x.IsFaulted)
    {
        Console.WriteLine(x.Exception?.Message);
    }
    else
    {
        Console.WriteLine("Seed data added successfully.");
    }
}); //seed data için threadleri bloklamadan arka planda çalışma görevi burası

app.AddCategoryGroupEndpointExt(app.AddVersionSetExt());
app.AddCourseGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();

