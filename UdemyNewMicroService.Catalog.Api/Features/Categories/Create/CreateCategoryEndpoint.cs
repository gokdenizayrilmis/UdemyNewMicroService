using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroService.Shared.Extensions;

namespace UdemyNewMicroService.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {
        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult());

            return group;
        }
    }
}
