using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.ValidationInterfaces;

public interface IBookValidationService
{
    Task<ValidationResult> ValidateBookAsync(BookDto bookDto, IFormFile file);
}