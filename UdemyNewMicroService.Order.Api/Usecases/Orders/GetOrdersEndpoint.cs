using MediatR;
using UdemyNewMicroService.Order.Application.Usecases.Orders.GetOrder;
using UdemyNewMicroService.Shared.Extensions;

namespace UdemyNewMicroService.Order.Api.Usecases.Orders
{
    public static class GetOrdersEndpoint
    {
        public static RouteGroupBuilder GetOrdersGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
                (await mediator.Send(new GetOrdersQuery())).ToGenericResult())
                .WithName("GetOrders")
                .MapToApiVersion(1, 0).RequireAuthorization("Password");

            return group;
        }
    } 
}
