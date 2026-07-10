using MediatR;
using System.Net;
using System.Text.Json;
using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

            if(string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            var basketItemToDelete = currentBasket!.Items.FirstOrDefault(i => i.Id == request.Id);

            if (basketItemToDelete == null)
            {
                return ServiceResult.Error("Item not found", HttpStatusCode.NotFound);
            }

            currentBasket.Items.Remove(basketItemToDelete);

            basketAsJson = JsonSerializer.Serialize(currentBasket);
            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
