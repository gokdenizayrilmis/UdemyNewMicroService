using MediatR;
using UdemyNewMicroService.Shared.Extensions;

namespace UdemyNewMicroService.File.Api.Features.File.Delete
{
    public static class DeleteFileCommandEndpoint
    {
        public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{fileName}", async (string fileName, IMediator mediator) =>
                (await mediator.Send(new DeleteFileCommand(fileName))).ToGenericResult())
                .WithName("delete")
                .MapToApiVersion(1, 0).RequireAuthorization("ClientCredential");

            return group;
        }
    }
}
