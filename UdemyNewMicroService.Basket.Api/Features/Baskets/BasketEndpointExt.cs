using Asp.Versioning.Builder;
using UdemyNewMicroService.Basket.Api.Features.Baskets.AddBasketItem;
using UdemyNewMicroService.Basket.Api.Features.Baskets.DeleteBasketItem;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets")
                .WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupEndpointExt()
                .DeleteBasketItemGroupItemEndpoint();
        }
    }
}
