using FluentValidation;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandValidator : AbstractValidator<ApplyDiscountCouponCommand>
    {
        public ApplyDiscountCouponCommandValidator()
        {
            RuleFor(x => x.Coupon).NotEmpty().WithMessage("Coupon code is required.");
            RuleFor(x => x.DiscountRate).GreaterThan(0).WithMessage("Discount rate must be greater than zero.");
        }
    }
}
