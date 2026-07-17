using System;
using System.Collections.Generic;
using System.Text;
using UdemyNewMicroService.Order.Application.Features.Orders.CreateOrder;

namespace UdemyNewMicroService.Order.Application.Features.Orders.GetOrder
{
    public record GetOrdersResponse(DateTime Created, decimal TotalPrice, List<OrderItemDto> Items)
    {
    }
}
