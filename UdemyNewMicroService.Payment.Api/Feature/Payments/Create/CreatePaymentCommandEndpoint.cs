using MediatR;
using UdemyNewMicroService.Payment.Api.Feature.Payments.Create;
using UdemyNewMicroService.Shared.Extensions;

namespace UdemyNewMicroService.Payment.Api.Features.Payments.Create
{
    public static class GetAllPaymentsCommandEndpoint
    {
        public static RouteGroupBuilder CreatePaymentGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreatePaymentCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
                .WithName("create")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
