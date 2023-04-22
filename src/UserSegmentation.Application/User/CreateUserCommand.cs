using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public class CreateUserRequest
{
  public string Username { get; set; } = "";
  public string Email { get; set; } = "";
}

public record CreateUserCommand(string Username, string Email) : IRequest<int>;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
{
  private readonly IRepository<Core.UserAggregate.User> _userRepository;

  public CreateUserHandler(IRepository<Core.UserAggregate.User> userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var user = new Core.UserAggregate.User(request.Username, request.Email);
    var createdUser = await _userRepository.AddAsync(user, cancellationToken);
    await _userRepository.SaveChangesAsync(cancellationToken);
    return createdUser.Id;
  }
}
