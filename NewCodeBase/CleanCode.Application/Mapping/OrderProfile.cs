using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
    }
}
