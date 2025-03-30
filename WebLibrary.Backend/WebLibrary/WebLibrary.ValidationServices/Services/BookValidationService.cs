using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.BackgroundService.ClamAV;
using WebLibrary.ValidationServices.ValidateRules;

namespace WebLibrary.ValidationServices.Services;

public class BookValidationService : IBookValidationService
{
    private readonly ScanFileForMalwareHelper _scanFileForMalwareHelper;
    private readonly IValidator<BookDto> _bookValidator;
    private readonly int _maxFileSizeMb;
    private readonly int _maxResolution;
    private readonly string[] _allowedFormats;
    private readonly string[] _forbiddenFormats;

    public BookValidationService(ScanFileForMalwareHelper scanFileForMalwareHelper, IConfiguration configuration)
    {
        _scanFileForMalwareHelper = scanFileForMalwareHelper;
        _bookValidator = new BookValidator();
        _maxFileSizeMb = int.Parse(configuration["BookAttachment:MaxFileSizeMB"]);
        _maxResolution = int.Parse(configuration["BookAttachment:MaxResolution"]);
        _allowedFormats = configuration.GetSection("BookAttachment:AllowedFormats").Get<string[]>() ?? Array.Empty<string>();
        _forbiddenFormats = configuration.GetSection("BookAttachment:ForbiddenFormats").Get<string[]>() ?? Array.Empty<string>();
    }

    public async Task<ValidationResult> ValidateBookAsync(BookDto bookDto, IFormFile file)
    {
        var validationResult = await _bookValidator.ValidateAsync(bookDto);
        if (!validationResult.IsValid)
        {
            return validationResult;
        }
        
        try
        {
            ValidateFileFormat(file);
            ValidateFileSize(file);
            await ValidateImageResolutionAsync(file);
            await _scanFileForMalwareHelper.ScanFileAsync(file);
        }
        catch (ValidationException ex)
        {
            return new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("File", ex.Message)
            });
        }
        
        return validationResult;
    }

    private void ValidateFileFormat(IFormFile file)
    {
        string extension = Path.GetExtension(file.FileName).ToLower();
        if (_forbiddenFormats.Contains(extension))
        {
            throw new ValidationException($"File format {extension} is not allowed");
        }
        if (!_allowedFormats.Contains(extension))
        {
            throw new ValidationException($"Not allowed format {extension}");
        }
    }

    private void ValidateFileSize(IFormFile file)
    {
        if (file.Length > _maxFileSizeMb * 1024 * 1024)
        {
            throw new ValidationException($"File size exceeds {_maxFileSizeMb}MB");
        }
    }

    private async Task ValidateImageResolutionAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        using var image = await Image.LoadAsync(stream);
        if (image.Width > _maxResolution || image.Height > _maxResolution)
        {
            throw new ValidationException($"Resolution bigger {_maxResolution}x{_maxResolution}");
        }
    }
}
