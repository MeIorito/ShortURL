namespace ShortURL.Validators;

using FluentValidation;
using ShortURL.DTOs.Auth;

public class LoginRequestValidator : AbstractValidator<LoginDto>
{
    public LoginRequestValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("We need your email to create an account.")
            .EmailAddress().WithMessage("Email must be a valid address.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password must not be blank.")
            .MinimumLength(8).WithMessage("Password must be 8 characters or longer.");
    }
}