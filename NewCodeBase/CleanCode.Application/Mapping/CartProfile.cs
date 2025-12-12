using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Mapping;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<Cart, CartDto>();
        CreateMap<CartDto, Cart>();
    }
}
