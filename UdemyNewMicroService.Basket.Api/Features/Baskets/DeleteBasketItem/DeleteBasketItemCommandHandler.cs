using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;
using UdemyNewMicroService.Basket.Api.Const;
using UdemyNewMicroService.Basket.Api.DTOs;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            Guid UserId = identityService.GetUserId;

            var cacheKey = string.Format(BasketConst.BasketCacheKey, UserId);

            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            if(string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

            var basketItemToDelete = currentBasket.BasketItem.FirstOrDefault(i => i.Id == request.CourseId);

            if (basketItemToDelete == null)
            {
                return ServiceResult.Error("Item not found", HttpStatusCode.NotFound);
            }

            currentBasket.BasketItem.Remove(basketItemToDelete);

            basketAsString = JsonSerializer.Serialize(currentBasket);
            await distributedCache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
