using AutoMapper;
using MediatR;
using UdemyNewMicroService.Order.Application.Contracts.Repositories;
using UdemyNewMicroService.Order.Application.Features.Orders.CreateOrder;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Order.Application.Features.Orders.GetOrder
{
    public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
    {
        public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrderByBuyerId(identityService.UserId);

            var response = orders.Select(x => new GetOrdersResponse(x.Created, x.TotalPrice, mapper.Map<List<OrderItemDto>>(x.OrderItems))).ToList();
            
            return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
        }
    }
}
        