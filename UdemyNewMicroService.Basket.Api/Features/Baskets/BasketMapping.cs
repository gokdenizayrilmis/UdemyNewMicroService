using AutoMapper;
using UdemyNewMicroService.Basket.Api.DTOs;

namespace UdemyNewMicroService.Basket.Api.Features.Baskets
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketDto, Data.Basket>().ReverseMap();
            CreateMap<BasketItemDto, Data.BasketItem>().ReverseMap();
        }
    }   
}
