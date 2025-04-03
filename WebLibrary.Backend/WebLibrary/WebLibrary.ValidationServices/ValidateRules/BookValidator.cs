using FluentValidation;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Requests;

namespace WebLibrary.ValidationServices.ValidateRules
{
    /// <summary>
    /// Валидатор для DTO книги.
    /// </summary>
    public class BookValidator : AbstractValidator<AddBookRequest>
    {
        public BookValidator()
        {
            RuleFor(b => b.ISBN)
                .NotEmpty().WithMessage("ISBN is required.")
                .Matches("^[0-9]{10,}$").WithMessage("ISBN must be at least 10 digits and contain only numbers.");
        }
    }
}