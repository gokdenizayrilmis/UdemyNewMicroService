using UdemyNewMicroService.Catalog.Api.Features.Courses;

namespace UdemyNewMicroService.Catalog.Api.Features.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course>? Courses { get; set; } = new List<Course>();
    }
}
