using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Application.Mappings;

public class UserMappingProfile: Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<UserDto, User>();
    }
}