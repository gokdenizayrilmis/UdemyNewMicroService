using UdemyNewMicroService.Catalog.Api.Features.Courses.DTOs;
using UdemyNewMicroService.Catalog.Api.Features.Courses.GetAll;
using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Catalog.Api.Features.Courses.GetById
{

    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;

    public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {

            var hascourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (hascourse is null)
            {
                return ServiceResult<CourseDto>.Error("Course not found", HttpStatusCode.NotFound);
            }

            var category = await context.Categories.FindAsync(hascourse.CategoryId, cancellationToken);

            hascourse.Category = category!;

            var courseAsDto = mapper.Map<CourseDto>(hascourse);
            return ServiceResult<CourseDto>.SuccessAsOk(courseAsDto);

        }
    }

    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCoursesGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
                (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult())
                .MapToApiVersion(1, 0)
                .WithName("GetByIdCourses").RequireAuthorization("ClientCredential");

            return group;
        }
    }

}
