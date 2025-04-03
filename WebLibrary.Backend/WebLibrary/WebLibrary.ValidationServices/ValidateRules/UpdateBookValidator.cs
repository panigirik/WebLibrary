using FluentValidation;
using WebLibrary.Application.Requests;

namespace WebLibrary.ValidationServices.ValidateRules;

/// <summary>
/// Валидатор для запроса на обновление книги.
/// </summary>
public class UpdateBookValidator : AbstractValidator<UpdateBookRequest>
{
    public UpdateBookValidator()
    {
        RuleFor(b => b.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches("^[0-9]{10,}$").WithMessage("ISBN must be at least 10 digits and contain only numbers.");

        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(b => b.Genre)
            .NotEmpty().WithMessage("Genre is required.");

        RuleFor(b => b.Description)
            .NotEmpty().WithMessage("Description is required.");
    }
}