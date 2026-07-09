using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.AddBasketItem
{
    public record AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string? ImageUrl) : IRequestByServiceResult;
}
