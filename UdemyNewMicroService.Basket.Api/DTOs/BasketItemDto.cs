namespace UdemyNewMicroService.Basket.Api.DTOs
{
    public record BasketItemDto(Guid Id, string Name, string ImageUrl, decimal Price, decimal? PriceByApplyDiscountRate)
    {
    }
}
