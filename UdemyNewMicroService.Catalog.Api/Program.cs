using UdemyNewMicroService.Catalog.Api;
using UdemyNewMicroService.Catalog.Api.Features.Categories;
using UdemyNewMicroService.Catalog.Api.Features.Courses;
using UdemyNewMicroService.Catalog.Api.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(); 
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
    // .NET 10'un /openapi/v1.json endpoint'ini aktif eder
    app.MapOpenApi();

    // Swagger UI'a yeni .json yolunu bildirir
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "UdemyNewMicroService.Catalog.Api v1");
    });
}


app.Run();

