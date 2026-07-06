using MongoDB.Driver;
using System.Runtime.CompilerServices;
using UdemyNewMicroService.Catalog.Api.Features.Categories;
using UdemyNewMicroService.Catalog.Api.Features.Courses;

namespace UdemyNewMicroService.Catalog.Api.Repositories
{
    public static class SeedData
    {
        public static async Task AddSeedDataExt(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
                
                if(!dbContext.Categories.Any()) 
                { 
                    var categories = new List<Category>
                    {
                        new () {Id = NewId.NextSequentialGuid(), Name = "Development"},
                        new () {Id = NewId.NextSequentialGuid(), Name = "Business"},
                        new () {Id = NewId.NextSequentialGuid(), Name = "IT & Software"},
                    };

                     dbContext.Categories.AddRange(categories);
                     await dbContext.SaveChangesAsync();
                }

                if (dbContext.Categories.Any())
                {
                    var category = await dbContext.Categories.FirstAsync();

                    var randomUserId = NewId.NextGuid();

                    List<Course> courses = new List<Course>
                    {
                        new () {Id = NewId.NextSequentialGuid(), Name = "C# Programming", Description = "Learn C# programming from scratch.", Price = 49.99m, UserId = randomUserId, CategoryId = category.Id},
                        new () {Id = NewId.NextSequentialGuid(), Name = "Business Strategy", Description = "Master business strategy and decision-making.", Price = 59.99m, UserId = randomUserId, CategoryId = category.Id},
                        new () {Id = NewId.NextSequentialGuid(), Name = "IT Security Fundamentals", Description = "Understand the basics of IT security.", Price = 39.99m, UserId = randomUserId, CategoryId = category.Id},
                    };

                    dbContext.Courses.AddRange(courses);
                    await dbContext.SaveChangesAsync();
                }

            }

        }

    }
}