using MediatR;
using UdemyNewMicroService.Shared.Extensions;

namespace UdemyNewMicroService.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public static class GetAllPaymentsByUserIdEndpoint
    {
        public static RouteGroupBuilder GetAllPaymentByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
                (await mediator.Send(new GetAllPaymentsByUserIdQuery())).ToGenericResult())
                .WithName("get-all-payments-by-userid")
                .MapToApiVersion(1, 0).RequireAuthorization("Password");

            return group;
        }
    }
}
