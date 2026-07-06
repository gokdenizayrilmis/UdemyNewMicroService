using UdemyNewMicroService.Catalog.Api.Features.Courses.Create;
using UdemyNewMicroService.Catalog.Api.Features.Courses.GetAll;
using UdemyNewMicroService.Catalog.Api.Features.Courses.GetById;

namespace UdemyNewMicroService.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/courses")
                .WithTags("Courses")
                .CreateCourseGroupItemEndpoint()
                .GetAllCoursesGroupItemEndpoint()
                .GetByIdCoursesGroupItemEndpoint();
        }
    }
}
