using UdemyNewMicroService.Catalog.Api.Features.Courses.Create;

namespace UdemyNewMicroService.Catalog.Api.Features.Courses
{
    public class CourseMapping : Profile 
    {
        public CourseMapping() 
        {
            CreateMap<CreateCourseCommand, Course>();
        }
    }
}
