using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid CourseId) : IRequestByServiceResult
    {
    }
}