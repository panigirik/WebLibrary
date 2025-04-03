using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Requests;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Application.Mappings;

/// <summary>
/// Профиль маппинга для сущности User.
/// </summary>
public class UserMappingProfile: Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<UpdateUserInfoRequest, User>();
        CreateMap<User, UpdateUserInfoRequest>();
    }
}
