using FluentValidation;

namespace UserSegmentation.Application.User;

public class UserValidator : AbstractValidator<CreateUserCommand>
{
  public UserValidator()
  {
    RuleFor(x => x.Username).NotEmpty();

    RuleFor(x => x.Email)
      .NotEmpty()
      .WithMessage("Email address is required")
      .EmailAddress()
      .WithMessage("A valid email is required");
  }
}
