using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Discount.Api.Features.Discounts.CreateDiscount
{
    public static class GetDiscountByCodeCommandEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateDiscountCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateDiscount")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<CreateDiscountCommandValidator>>();

            return group;
        }
    }
}
