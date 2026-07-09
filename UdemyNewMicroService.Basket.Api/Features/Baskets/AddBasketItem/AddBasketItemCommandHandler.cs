using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using UdemyNewMicroService.Basket.Api.Const;
using UdemyNewMicroService.Basket.Api.DTOs;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid UserId = identityService.GetUserId;
            var cacheKey = string.Format(BasketConst.BasketCacheKey, UserId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            BasketDto? currentBasket;

            var newBasketItem = new BasketItemDto(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new BasketDto(UserId, [newBasketItem]);

                await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            } 
            
            currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

            var existingItem = currentBasket.BasketItem.FirstOrDefault(i => i.Id == newBasketItem.Id);

            
            if (existingItem is not null)
            {
                currentBasket.BasketItem.Remove(existingItem);
            }

            currentBasket.BasketItem.Add(newBasketItem);

            await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
            return ServiceResult.SuccessAsNoContent();

        }

        private async Task CreateCacheAsync(BasketDto basket, string cacheKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
        }

    }
}
