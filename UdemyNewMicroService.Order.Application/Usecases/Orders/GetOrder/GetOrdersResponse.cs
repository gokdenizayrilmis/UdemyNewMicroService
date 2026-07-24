using System;
using System.Collections.Generic;
using System.Text;
using UdemyNewMicroService.Order.Application.Usecases.Orders.CreateOrder;

namespace UdemyNewMicroService.Order.Application.Usecases.Orders.GetOrder
{
    public record GetOrdersResponse(DateTime Created, decimal TotalPrice, List<OrderItemDto> Items)
    {
    }
}
