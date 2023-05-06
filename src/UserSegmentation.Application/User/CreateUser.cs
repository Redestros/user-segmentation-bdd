using FluentValidation;
using LanguageExt.Common;
using MediatR;
using UserSegmentation.SharedKernel.Interfaces;
using ValidationException = FluentValidation.ValidationException;

namespace UserSegmentation.Application.User;

public class CreateUserRequest
{
  public string Username { get; set; } = "";
  public string Email { get; set; } = "";
}

public record CreateUserResponse(int Id);

public record CreateUserCommand(string Username, string Email) : IRequest<Result<int>>;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
  private readonly IRepository<Core.UserAggregate.User> _userRepository;
  private readonly IValidator<Core.UserAggregate.User> _validator;

  public CreateUserHandler(IRepository<Core.UserAggregate.User> userRepository, IValidator<Core.UserAggregate.User> validator)
  {
    _userRepository = userRepository;
    _validator = validator;
  }

  public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var user = new Core.UserAggregate.User(request.Username, request.Email);

    var validationResult = await _validator.ValidateAsync(user, cancellationToken);
    if (!validationResult.IsValid)
    {
      var validationException = new ValidationException(validationResult.Errors);
      return new Result<int>(validationException);
    }
    
    var createdUser = await _userRepository.AddAsync(user, cancellationToken);
    await _userRepository.SaveChangesAsync(cancellationToken);
    return createdUser.Id;  
  }
}
