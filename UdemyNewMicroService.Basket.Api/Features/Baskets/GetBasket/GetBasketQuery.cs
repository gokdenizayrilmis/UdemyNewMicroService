using UdemyNewMicroService.Basket.Api.DTOs;
using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery : IRequestByServiceResult<BasketDto>
    {
    }
}
