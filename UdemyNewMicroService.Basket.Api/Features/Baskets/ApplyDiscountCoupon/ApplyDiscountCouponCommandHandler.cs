using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;
using UdemyNewMicroService.Basket.Api.Const;
using UdemyNewMicroService.Basket.Api.DTOs;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(IIdentityService identityService, IDistributedCache distributedCache) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);
            var basketAsJson = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            if (!basket.Items.Any()) 
            {
                return ServiceResult<BasketDto>.Error("Basket item not found", HttpStatusCode.NotFound);
            }

            basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

            basketAsJson = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsJson, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
