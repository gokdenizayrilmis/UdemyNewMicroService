using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>
    {
    }
}
