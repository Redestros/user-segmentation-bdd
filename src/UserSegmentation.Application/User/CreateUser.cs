using FluentValidation;
using LanguageExt.Common;
using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public record CreateUserRequest(string Firstname,
  string Lastname,
  string Username,
  string Email,
  string PhoneNumber,
  DateOnly Birthdate);

public record CreateUserResponse(int Id);

public record CreateUserCommand(
  string Username,
  string Firstname,
  string Lastname,
  string Email,
  string PhoneNumber,
  DateOnly Birthdate) : IRequest<Result<CreateUserResponse>>;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<CreateUserResponse>>
{
  private readonly IRepository<Core.UserAggregate.User> _repository;
  private readonly IValidator<Core.UserAggregate.User> _validator;

  public CreateUserHandler(IRepository<Core.UserAggregate.User> repository,
    IValidator<Core.UserAggregate.User> validator)
  {
    _repository = repository;
    _validator = validator;
  }

  public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
  {
    var user = new Core.UserAggregate.User(
      command.Username,
      command.Firstname,
      command.Lastname,
      command.Email,
      command.PhoneNumber,
      command.Birthdate
    );

    var validationResult = await _validator.ValidateAsync(user, cancellationToken);
    if (!validationResult.IsValid)
    {
      var validationException = new ValidationException(validationResult.Errors);
      return new Result<CreateUserResponse>(validationException);
    }

    var createdUser = await _repository.AddAsync(user, cancellationToken);
    return new CreateUserResponse(createdUser.Id);
  }
}
