using System.Text.Json.Serialization;

namespace UdemyNewMicroService.Basket.Api.DTOs
{
    public record BasketDto
    {
        [JsonIgnore] public Guid UserId { get; init; }
        public List<BasketItemDto> Items { get; set; } = new();

        public BasketDto(Guid userId, List<BasketItemDto> items)
        {
            UserId = userId;
            Items = items;
        }
        public BasketDto() 
        {
        }

    }
}
