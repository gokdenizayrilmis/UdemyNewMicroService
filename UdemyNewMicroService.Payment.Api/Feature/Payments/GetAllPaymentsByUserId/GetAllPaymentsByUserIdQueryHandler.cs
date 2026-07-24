using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroService.Payment.Api.Repositories;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public class GetAllPaymentsByUserIdQueryHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<GetAllPaymentsByUserIdQuery, ServiceResult<List<GetAllPaymentsByUserIdResponse>>>
    {
        public async Task<ServiceResult<List<GetAllPaymentsByUserIdResponse>>> Handle(GetAllPaymentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId;

            var payments = await context.Payments
                .Where(p => p.UserId == userId)
                .Select(p => new GetAllPaymentsByUserIdResponse(p.Id, p.OrderCode, p.Amount.ToString("C"), p.Created, p.Status))
                .ToListAsync();

            return ServiceResult<List<GetAllPaymentsByUserIdResponse>>.SuccessAsOk(payments);
        }
    }
}
