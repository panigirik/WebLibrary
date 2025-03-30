using FluentValidation;
using WebLibrary.Application.Dtos;

namespace WebLibrary.ValidationServices.ValidateRules;

public class BookValidator : AbstractValidator<BookDto>
{
    public BookValidator()
    {
        RuleFor(b => b.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches("^[0-9]{10,}$").WithMessage("ISBN must be at least 10 digits and contain only numbers.");
    }
}