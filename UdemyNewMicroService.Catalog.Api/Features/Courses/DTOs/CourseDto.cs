namespace UdemyNewMicroService.Catalog.Api.Features.Courses.DTOs
{
    public record CourseDto(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        CategoryDto Category,
        FeatureDto Feature);
}
