using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroService.Shared.Extensions;
using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/item", async ([FromBody]DeleteBasketItemCommand command, [FromServices] IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
                .WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<DeleteBasketItemCommandValidator>>();

            return group;
        }
    }
}
