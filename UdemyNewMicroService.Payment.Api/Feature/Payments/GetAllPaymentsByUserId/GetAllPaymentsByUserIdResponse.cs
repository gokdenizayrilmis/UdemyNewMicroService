using UdemyNewMicroService.Payment.Api.Repositories;

namespace UdemyNewMicroService.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdResponse(Guid Id, string OrderCode, string Amount, DateTime Created, PaymentStatus Status);
}
