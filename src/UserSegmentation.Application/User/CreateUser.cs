using FluentValidation;
using LanguageExt.Common;
using MediatR;
using UserSegmentation.Core.Interfaces;

namespace UserSegmentation.Application.User;

public class CreateUserRequest
{
  public string Username { get; set; } = "";
  public string Email { get; set; } = "";
}

public record CreateUserResponse(int Id);

public record CreateUserCommand(string Username, string Email) : IRequest<Result<CreateUserResponse>>;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<CreateUserResponse>>
{
  private readonly ICreateUserService _userService;
  private readonly IValidator<Core.UserAggregate.User> _validator;

  public CreateUserHandler(ICreateUserService userService, IValidator<Core.UserAggregate.User> validator)
  {
    _userService = userService;
    _validator = validator;
  }

  public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
  {
    var user = new Core.UserAggregate.User(command.Username, command.Email);

    var validationResult = await _validator.ValidateAsync(user, cancellationToken);
    if (!validationResult.IsValid)
    {
      var validationException = new ValidationException(validationResult.Errors);
      return new Result<CreateUserResponse>(validationException);
    }

    var createdUserId = await _userService.CreateUser(command.Username, command.Email);
    return new CreateUserResponse(createdUserId);  
  }
}
