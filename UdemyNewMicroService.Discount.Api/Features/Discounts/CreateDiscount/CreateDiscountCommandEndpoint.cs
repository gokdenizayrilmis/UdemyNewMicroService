using UdemyNewMicroService.Discount.Api.Features.Discounts.CreateDiscount;
using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Discount.Api.Features.Discounts
{
    public static class CreateDiscountCommandEndpoint
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
