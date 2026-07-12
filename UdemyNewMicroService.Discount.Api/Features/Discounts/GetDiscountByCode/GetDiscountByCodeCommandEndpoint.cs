using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Discount.Api.Features.Discounts.GetDiscountByCode
{
    public static class GetDiscountByCodeCommandEndpoint
    {
        public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{code}", async (string code, IMediator mediator) =>
                (await mediator.Send(new GetDiscountByCodeQuery(code))).ToGenericResult())
                .WithName("GetDiscountByCode")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
