using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UdemyNewMicroService.Catalog.Api.Features.Categories.Create;
using UdemyNewMicroService.Catalog.Api.Options;
using UdemyNewMicroService.Catalog.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();

var app = builder.Build();

app.MapPost("/categories", async (CreateCategoryCommand command, IMediator mediator) =>
{
    var result = await mediator.Send(command);

    return new ObjectResult(result)
    {
        StatusCode = result.Status.GetHashCode(),
    };

});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();

