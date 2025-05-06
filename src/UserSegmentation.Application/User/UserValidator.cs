using Ardalis.Specification;
using FluentValidation;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public class CreateUserValidator : AbstractValidator<Core.UserAggregate.User>
{
  private readonly IRepository<Core.UserAggregate.User> _repository;
  public CreateUserValidator(IRepository<Core.UserAggregate.User> repository)
  {
    _repository = repository; 
    
    RuleFor(x => x.Email)
      .NotEmpty()
      .WithMessage("Email address is required")
      .EmailAddress()
      .WithMessage("A valid email is required");

    RuleFor(x => x.Username)
      .NotEmpty()
      .MustAsync((x, _) => ValidateUsername(x))
      .WithMessage("Username already used");
  }

  private async Task<bool> ValidateUsername(string username)
  {
    var spec = new GetUsername(username);
    var existingId = await _repository.FirstOrDefaultAsync(spec);
    
    return !existingId.HasValue;
  }
}

internal sealed class GetUsername : Specification<Core.UserAggregate.User, int?>, ISingleResultSpecification<Core.UserAggregate.User>
{
  public GetUsername(string username)
  {
    Query.Where(x => x.Username.Equals(username));

    Query.Select(x => x.Id);
  }
}
