using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}
