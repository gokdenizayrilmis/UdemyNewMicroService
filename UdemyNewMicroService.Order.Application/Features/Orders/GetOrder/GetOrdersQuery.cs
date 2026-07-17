using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Order.Application.Features.Orders.GetOrder
{
    public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>
    {
    }
}
