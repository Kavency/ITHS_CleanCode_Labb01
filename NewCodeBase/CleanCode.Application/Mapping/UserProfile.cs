using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Application.Models;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserProfileDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<LoginRequest, User>();
    }
}
