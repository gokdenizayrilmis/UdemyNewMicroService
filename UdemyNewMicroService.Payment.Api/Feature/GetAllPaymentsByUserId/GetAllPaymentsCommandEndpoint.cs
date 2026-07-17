using MediatR;
using UdemyNewMicroService.Payment.Api.Feature.GetAllPaymentsByUserId;
using UdemyNewMicroService.Shared.Extensions;

namespace UdemyNewMicroService.Payment.Api.Features.GetAllPaymentsByUserId
{
    public static class GetAllPaymentsCommandEndpoint
    {
        public static RouteGroupBuilder GetAllPaymentByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
                (await mediator.Send(new GetAllPaymentsByUserIdQuery())).ToGenericResult())
                .WithName("get-all-payments-by-userid")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
