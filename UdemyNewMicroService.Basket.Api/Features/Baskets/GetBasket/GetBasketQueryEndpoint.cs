using MediatR;
using UdemyNewMicroService.Basket.Api.Features.Baskets.AddBasketItem;
using UdemyNewMicroService.Shared.Extensions;
using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.GetBasket
{
    public static class GetBasketQueryEndpoint
    {
        public static RouteGroupBuilder GetBasketGroupEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async (IMediator mediator) =>
                (await mediator.Send(new GetBasketQuery())).ToGenericResult())
                .WithName("GetBasket")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
