namespace UdemyNewMicroService.Basket.Api.DTOs
{
    public record BasketDto(Guid UserId, List<BasketItemDto> BasketItem)
    {
    }
}
