using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Application.Mappings;

public class RefreshTokenMappingProfile: Profile
{
    public RefreshTokenMappingProfile()
    {
        CreateMap<RefreshToken, RefreshTokenDto>();

        CreateMap<RefreshTokenDto, RefreshToken>();
    }
}