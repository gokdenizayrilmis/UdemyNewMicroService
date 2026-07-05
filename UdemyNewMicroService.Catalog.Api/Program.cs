using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UdemyNewMicroService.Catalog.Api;
using UdemyNewMicroService.Catalog.Api.Features.Categories;
using UdemyNewMicroService.Catalog.Api.Features.Categories.Create;
using UdemyNewMicroService.Catalog.Api.Features.Courses;
using UdemyNewMicroService.Catalog.Api.Options;
using UdemyNewMicroService.Catalog.Api.Repositories;
using UdemyNewMicroService.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));


var app = builder.Build();

app.AddCategoryGroupEndpointExt();
app.AddCourseGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();

