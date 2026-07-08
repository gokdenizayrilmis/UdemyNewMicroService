using UdemyNewMicroService.Catalog.Api.Features.Courses.GetById;

namespace UdemyNewMicroService.Catalog.Api.Features.Courses.Delete
{

    public record DeleteCourseCommand(Guid Id) : IRequestByServiceResult;

    public class DeleteCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FindAsync(request.Id);

            if (course is null)
            {
                return ServiceResult.ErrorAsNotFound();
            }

            context.Remove(course);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent(); 

        }
    }

    public static class DeleteCourseEndpoint
    {
        public static RouteGroupBuilder DeleteCoursesGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) =>
                (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult())
                .MapToApiVersion(1,0)
                .WithName("DeleteCourses");

            return group;
        }
    }

}