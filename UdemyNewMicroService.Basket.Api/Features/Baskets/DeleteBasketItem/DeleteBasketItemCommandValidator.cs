using FluentValidation;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required.");
        }
    }
}
