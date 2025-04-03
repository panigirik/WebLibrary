using FluentValidation;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.ValidationServices.ValidateRules;

namespace WebLibrary.ValidationServices.UseCases;

    /// <summary>
    /// Сервис валидации для обновления книги.
    /// </summary>
    public class UpdateBookUseCaseValidation : IUpdateBookUseCaseValidation
    {
        private readonly AbstractValidator<UpdateBookRequest> _validator;

        public UpdateBookUseCaseValidation()
        {
            _validator = new UpdateBookValidator();
        }

        /// <summary>
        /// Выполняет валидацию запроса на обновление книги.
        /// </summary>
        public async Task ValidateAsync(UpdateBookRequest updateBookRequest)
        {
            var validationResult = await _validator.ValidateAsync(updateBookRequest);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
