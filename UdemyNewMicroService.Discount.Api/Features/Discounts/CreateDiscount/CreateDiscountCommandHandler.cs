using UdemyNewMicroService.Discount.Api.Repositories;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = new Discount()
            {
                Id = NewId.NextSequentialGuid(),
                Code = request.Code,
                Created = DateTime.UtcNow,
                Expired = request.Expired,
                Rate = request.Rate,
                UserId = identityService.GetUserId
            };

            await context.Discounts.AddAsync(discount, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
