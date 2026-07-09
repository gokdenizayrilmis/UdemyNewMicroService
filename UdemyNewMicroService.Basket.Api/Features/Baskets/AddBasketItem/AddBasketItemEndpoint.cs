using MediatR;
using UdemyNewMicroService.Shared.Extensions;
using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateBasketItem")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommandValidator>>();

            return group;adss
        }   
    }
}
