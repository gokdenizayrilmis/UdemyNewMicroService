using UdemyNewMicroService.Catalog.Api.Features.Courses.Create;
using UdemyNewMicroService.Catalog.Api.Features.Courses.DTOs;

namespace UdemyNewMicroService.Catalog.Api.Features.Courses
{
    public class CourseMapping : Profile 
    {
        public CourseMapping() 
        {
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}
