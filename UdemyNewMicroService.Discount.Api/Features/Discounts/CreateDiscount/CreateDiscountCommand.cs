namespace UdemyNewMicroService.Discount.Api.Features.Discounts.CreateDiscount
{
    public record CreateDiscountCommand(string Code, float Rate, Guid UserId, DateTime Expired) : IRequest<ServiceResult>
    {
    }
}
