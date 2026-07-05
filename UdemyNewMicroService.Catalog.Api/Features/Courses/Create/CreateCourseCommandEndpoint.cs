using UdemyNewMicroService.Catalog.Api.Features.Categories.Create;
using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseCommandEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCourseCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateCourse")
                .Produces<Guid>(StatusCodes.Status201Created)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return group;
        }
    }
}
