using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using UdemyNewMicroService.Basket.Api.Const;
using UdemyNewMicroService.Basket.Api.Data;
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

            Data.Basket? currentBasket;

            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsString))
            {
                currentBasket = new Data.Basket(UserId, [newBasketItem]);

                await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            } 
            
            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);

            var existingItem = currentBasket!.Items.FirstOrDefault(i => i.Id == newBasketItem.Id);

            
            if (existingItem is not null)
            {
                currentBasket.Items.Remove(existingItem);
            }

            currentBasket.Items.Add(newBasketItem);

            await CreateCacheAsync(currentBasket, cacheKey, cancellationToken);
            return ServiceResult.SuccessAsNoContent();

        }

        private async Task CreateCacheAsync(Data.Basket basket, string cacheKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
        }

    }
}
