using Asp.Versioning.Builder;
using UdemyNewMicroService.Basket.Api.Features.Baskets.AddBasketItem;
using UdemyNewMicroService.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using UdemyNewMicroService.Basket.Api.Features.Baskets.DeleteBasketItem;
using UdemyNewMicroService.Basket.Api.Features.Baskets.GetBasket;
using UdemyNewMicroService.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketGroupItemEndpointExt()
                .DeleteBasketGroupItemEndpoint()
                .GetBasketGroupEndpointExt()
                .ApplyDiscountCouponGroupItemEndpointExt()
                .RemoveDiscountCouponGroupItemEndpointExt();
        }
    }
}
