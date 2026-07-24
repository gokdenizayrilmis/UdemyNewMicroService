using Asp.Versioning.Builder;
using UdemyNewMicroService.Catalog.Api.Features.Courses.Create;
using UdemyNewMicroService.Catalog.Api.Features.Courses.Delete;
using UdemyNewMicroService.Catalog.Api.Features.Courses.GetAll;
using UdemyNewMicroService.Catalog.Api.Features.Courses.GetAllByUserId;
using UdemyNewMicroService.Catalog.Api.Features.Courses.GetById;
using UdemyNewMicroService.Catalog.Api.Features.Courses.Update;

namespace UdemyNewMicroService.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses")
                .WithTags("Courses")
                .WithApiVersionSet(apiVersionSet)
                .CreateCourseGroupItemEndpoint()
                .GetAllCoursesGroupItemEndpoint()
                .GetByIdCoursesGroupItemEndpoint()
                .UpdateCourseGroupItemEndpoint()
                .DeleteCoursesGroupItemEndpoint()
                .GetByUserIdCoursesGroupItemEndpoint().RequireAuthorization("ClientCredential");
        }
    }
}
