using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection;
using UdemyNewMicroService.Catalog.Api.Features.Categories;
using UdemyNewMicroService.Catalog.Api.Features.Courses;

namespace UdemyNewMicroService.Catalog.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
