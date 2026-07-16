using MediatR;
using UdemyNewMicroService.Basket.Api.Features.Baskets.AddBasketItem;
using UdemyNewMicroService.Shared.Extensions;
using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public static class ApplyDiscountEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount-coupon", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
                .WithName("ApplyDiscountCoupon")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();

            return group;
        }
    }
}
