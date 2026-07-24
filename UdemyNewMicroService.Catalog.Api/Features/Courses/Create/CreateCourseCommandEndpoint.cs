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
                .MapToApiVersion(1,0)
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces<Guid>(StatusCodes.Status404NotFound)
                .Produces<Guid>(StatusCodes.Status400BadRequest)
                .Produces<Guid>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>().RequireAuthorization("ClientCredential");

            return group;
        }
    }
}
