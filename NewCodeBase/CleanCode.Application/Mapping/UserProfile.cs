using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserProfileDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<LoginUserDto, User>();
    }
}
