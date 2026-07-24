using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyNewMicroService.Order.Application.Usecases.Orders.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator() 
        {
            RuleFor(x => x.DiscountRate).NotNull().GreaterThan(0).WithMessage("Discount rate must be greater than or equal to 0.");
            RuleFor(x => x.Address).NotNull().WithMessage("Address is required.").SetValidator(new AddressDtoValidator());
            RuleFor(x => x.Payment).NotNull().WithMessage("Payment is required.").SetValidator(new PaymentDtoValidator());
            RuleForEach(x => x.Items).NotNull().WithMessage("Items are required.").SetValidator(new OrderItemDtoValidator());
        }
    }

    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.Province).NotEmpty().WithMessage("Province is required.");
            RuleFor(x => x.District).NotEmpty().WithMessage("District is required.");
            RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("ZipCode is required.");
            RuleFor(x => x.Line).NotEmpty().WithMessage("Line is required.");
        }
    }

    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("ProductName is required.");
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("UnitPrice must be greater than 0.");
        }
    }

    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("CardHolderName is required.");
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("CardNumber is required.");
            RuleFor(x => x.Expiration).NotEmpty().WithMessage("Expiration is required.");
            RuleFor(x => x.Cvc).NotEmpty().WithMessage("Cvc is required.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");
        }
    }

}
