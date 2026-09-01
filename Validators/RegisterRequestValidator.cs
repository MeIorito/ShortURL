namespace ShortURL.Validators;

using FluentValidation;
using ShortURL.DTOs.Auth;

public class RegisterRequestValidator : AbstractValidator<RegisterDto>
{
    public RegisterRequestValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("We need your email to create an account.")
            .EmailAddress().WithMessage("Email must be a valid address.");

        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username must not be blank");

        RuleFor(user => user.FullName)
            .NotEmpty().WithMessage("Full name must not be blank");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password must not be blank.")
            .MinimumLength(8).WithMessage("Password must be 8 characters or longer.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");
    }
}