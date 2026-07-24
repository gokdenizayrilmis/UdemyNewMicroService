using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNewMicroService.Order.Application.Usecases.Orders.CreateOrder;
using UdemyNewMicroService.Order.Domain.Entities;

namespace UdemyNewMicroService.Order.Application.Usecases.Orders
{
    public class OrderMapping : Profile
    {
        public OrderMapping() 
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
