using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroService.Shared.Extensions;
using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/item/{id:guid}", async (Guid id, IMediator mediator) =>
                (await mediator.Send(new DeleteBasketItemCommand(id))).ToGenericResult())
                .WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0).RequireAuthorization("Password");

            return group;
        }
    }
}
