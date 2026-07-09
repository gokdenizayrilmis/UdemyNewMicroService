using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;
using UdemyNewMicroService.Basket.Api.Const;
using UdemyNewMicroService.Basket.Api.DTOs;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityService.GetUserId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if (string.IsNullOrEmpty(basketAsString)) 
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var basket = JsonSerializer.Deserialize<BasketDto>(basketAsString);
            return ServiceResult<BasketDto>.SuccessAsOk(basket);
        }
    }
}
