using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.BackgroundService.ClamAV;
using WebLibrary.ValidationServices.ValidateRules;
using ValidationException = FluentValidation.ValidationException;

namespace WebLibrary.ValidationServices.Services;

    /// <summary>
    /// Сервис для валидации книг.
    /// </summary>
    public class BookValidationService : IBookValidationService
    {
        private readonly ScanFileForMalwareHelper _scanFileForMalwareHelper;
        private readonly IValidator<AddBookRequest> _bookValidator;
        private readonly int _maxFileSizeMb;
        private readonly int _maxResolution;
        private readonly string[] _allowedFormats;
        private readonly string[] _forbiddenFormats;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BookValidationService"/>.
        /// </summary>
        /// <param name="scanFileForMalwareHelper">Сервис для проверки файлов на вирусы.</param>
        /// <param name="configuration">Конфигурация приложения.</param>
        public BookValidationService(ScanFileForMalwareHelper scanFileForMalwareHelper, IConfiguration configuration)
        {
            _scanFileForMalwareHelper = scanFileForMalwareHelper;
            _bookValidator = new BookValidator();
            _maxFileSizeMb = int.Parse(configuration["BookAttachment:MaxFileSizeMB"]);
            _maxResolution = int.Parse(configuration["BookAttachment:MaxResolution"]);
            _allowedFormats = configuration.GetSection("BookAttachment:AllowedFormats").Get<string[]>() ?? Array.Empty<string>();
            _forbiddenFormats = configuration.GetSection("BookAttachment:ForbiddenFormats").Get<string[]>() ?? Array.Empty<string>();
        }

        /// <summary>
        /// Выполняет валидацию книги и её файла.
        /// </summary>
        /// <param name="bookDto">DTO книги.</param>
        /// <param name="file">Файл, прикрепленный к книге.</param>
        /// <returns>Результат валидации.</returns>
        public async Task<ValidationResult> ValidateBookAsync(AddBookRequest bookRequest, IFormFile file)
        {
            var validationResult = await _bookValidator.ValidateAsync(bookRequest);
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

        /// <summary>
        /// Проверяет формат файла.
        /// </summary>
        /// <param name="file">Файл, прикрепленный к книге.</param>
        private void ValidateFileFormat(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName).ToLower();
            if (_forbiddenFormats.Contains(extension))
            {
                throw new ValidationException($"File format {extension} is not allowed");
            }
            if (!_allowedFormats.Contains(extension))
            {
                throw new ForbiddenException($"Not allowed format {extension}");
            }
        }

        /// <summary>
        /// Проверяет размер файла.
        /// </summary>
        /// <param name="file">Файл, прикрепленный к книге.</param>
        private void ValidateFileSize(IFormFile file)
        {
            if (file.Length > _maxFileSizeMb * 1024 * 1024)
            {
                throw new ValidationException($"File size exceeds {_maxFileSizeMb}MB");
            }
        }

        /// <summary>
        /// Проверяет разрешение изображения в файле.
        /// </summary>
        /// <param name="file">Файл, прикрепленный к книге.</param>
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

