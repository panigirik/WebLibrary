using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Requests;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Application.Mappings;

/// <summary>
/// Профиль маппинга для сущности Book.
/// </summary>
public class BookMappingProfile: Profile
{
    public BookMappingProfile()
    {
        CreateMap<BookDto, Book>();
        CreateMap<Book, BookDto>();
        CreateMap<GetBookRequestDto, Book>();
        CreateMap<Book, GetBookRequestDto>();
        CreateMap<Book, AddBookRequest>();
        CreateMap<AddBookRequest, Book>();
        CreateMap<UpdateBookRequest, Book>();
        CreateMap<Book, UpdateBookRequest>();
    }
}