using MediatR;
using UdemyNewMicroService.Payment.Api.Repositories;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Payment.Api.Feature.Payments.Create
{
    public class CreatePaymentCommandHandler(AppDbContext appDbContext, IIdentityService identityService, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreatePaymentCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var claims = httpContextAccessor.HttpContext.User.Claims;

            var (isSuccess, errorMessage) = await ExternalPaymentProcessAsync(request.CardNumber, request.CardHolderName, request.CardExpirationDate, request.CardSecurityNumber, request.Amount);

            if (!isSuccess)
            {
                return ServiceResult<Guid>.Error("Payment processing failed", errorMessage ?? "Unknown error", System.Net.HttpStatusCode.BadRequest);
            }

            var userId = identityService.UserId;

            var newPayment = new Repositories.Payment(userId, request.OrderCode, request.Amount);
            newPayment.SetStatus(PaymentStatus.Success);

            appDbContext.Payments.Add(newPayment);
            await appDbContext.SaveChangesAsync(cancellationToken);
            
            return ServiceResult<Guid>.SuccessAsOk(newPayment.Id);
        }

        private async Task<(bool isSuccess, string? errorMessage)> ExternalPaymentProcessAsync(string cardNumber, string cardHolderName, string cardExpirationDate, string cardSecurityNumber, decimal amount)
        {
            // Simulate external payment processing
            await Task.Delay(1000); // Simulate some delay
            return (true, "Payment successful."); // Assume payment is always successful for this example
        }

    }
}
