using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Application.Mappings;

public class BookMappingProfile: Profile
{
    public BookMappingProfile()
    {
        CreateMap<BookDto, Book>();

        CreateMap<Book, BookDto>();

        CreateMap<GetBookRequestDto, Book>();

        CreateMap<Book, GetBookRequestDto>();
    }
}