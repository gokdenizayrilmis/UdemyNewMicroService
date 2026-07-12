using UdemyNewMicroService.Discount.Api.Repositories;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {

            var hasCodeForUser = await context.Discounts.AnyAsync(x => x.UserId == request.UserId && x.Code == request.Code);

            if (hasCodeForUser) 
            {
                return ServiceResult.Error("Discount code already exists for this user.", HttpStatusCode.BadRequest);
            }

            var discount = new Discount()
            {
                Id = NewId.NextSequentialGuid(),
                Code = request.Code,
                Created = DateTime.UtcNow,
                Expired = request.Expired,
                Rate = request.Rate,
                UserId = request.UserId
            };

            await context.Discounts.AddAsync(discount, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
