using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Application.Mappings;

/// <summary>
/// Профиль маппинга для сущности Author.
/// </summary>
public class AuthorMappingProfile: Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorDto, Author>();
    }
}
