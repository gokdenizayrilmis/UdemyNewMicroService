using MediatR;
using System.Text.Json;
using UdemyNewMicroService.Basket.Api.Data;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IIdentityService identityService, BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            Data.Basket? currentBasket;

            var newBasketItem = new BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice, null);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                currentBasket = new Data.Basket(identityService.UserId, [newBasketItem]);

                await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            } 
            
            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            var existingItem = currentBasket!.Items.FirstOrDefault(i => i.Id == newBasketItem.Id);

            
            if (existingItem is not null)
            {
                currentBasket.Items.Remove(existingItem);
            }

            currentBasket.Items.Add(newBasketItem);

            currentBasket.ApplyAvailableDiscount();

            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();

        }

    }
}
