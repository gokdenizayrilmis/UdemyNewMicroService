using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate) : IRequestByServiceResult
    {
    }
}
