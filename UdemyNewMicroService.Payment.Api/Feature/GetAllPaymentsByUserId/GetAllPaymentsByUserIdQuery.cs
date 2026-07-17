using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Payment.Api.Feature.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>
    {
    }
}
