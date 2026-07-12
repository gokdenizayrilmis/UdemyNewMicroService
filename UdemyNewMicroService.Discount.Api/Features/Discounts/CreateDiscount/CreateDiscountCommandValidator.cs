namespace UdemyNewMicroService.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");
            RuleFor(x => x.Rate)
                .NotEmpty().WithMessage("Rate is required.");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.Expired)
                .NotEmpty().WithMessage("Expired date is required.");
        }
    }
}
