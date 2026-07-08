using UdemyNewMicroService.Catalog.Api.Features.Courses.DTOs;

namespace UdemyNewMicroService.Catalog.Api.Features.Courses.GetAllByUserId
{
    public record GetCourseByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;

    public class GetCourseByUserIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByUserIdQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetCourseByUserIdQuery request, CancellationToken cancellationToken)
        {

            var courses = await context.Courses.Where(c => c.UserId == request.Id).ToListAsync(cancellationToken);

            var categories = await context.Categories.ToListAsync(cancellationToken);

            foreach (var course in courses)
            {
                course.Category = categories.FirstOrDefault(c => c.Id == course.CategoryId);
            }

            var courseDtos = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOk(courseDtos);

        }
    }

    public static class GetCourseByUserIdEndpoint
    {
        public static RouteGroupBuilder GetByUserIdCoursesGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}", async (IMediator mediator, Guid userId) =>
                (await mediator.Send(new GetCourseByUserIdQuery(userId))).ToGenericResult())
                .MapToApiVersion(1, 0)
                .WithName("GetByUserIdCourses");

            return group;
        }
    }

}
