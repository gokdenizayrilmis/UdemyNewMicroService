using FluentValidation;

namespace UdemyNewMicroService.Payment.Api.Feature.Payments.Create
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator() 
        {
            RuleFor(x => x.OrderCode)
                .NotEmpty().WithMessage("Order code is required.")
                .Length(1, 10).WithMessage("Order code must be between 1 and 10 characters.");
            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Card number is required.")
                .CreditCard().WithMessage("Invalid card number.");
            RuleFor(x => x.CardHolderName)
                .NotEmpty().WithMessage("Card holder name is required.");
            RuleFor(x => x.CardExpirationDate)
                .NotEmpty().WithMessage("Card expiration date is required.")
                .Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$").WithMessage("Invalid expiration date format. Use MM/YY.");
            RuleFor(x => x.CardSecurityNumber)
                .NotEmpty().WithMessage("Card security number is required.")
                .Matches(@"^\d{3,4}$").WithMessage("Invalid security number. It should be 3 or 4 digits.");
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}
