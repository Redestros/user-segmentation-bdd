using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.User;

public record GetUsersQuery : IRequest<List<UserDto>>;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
  private readonly IRepository<Core.UserAggregate.User> _repository;

  public GetUsersHandler(IRepository<Core.UserAggregate.User> repository)
  {
    _repository = repository;
  }

  public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
  {
    var users = await _repository.ListAsync(cancellationToken);
    return users.Select(user => new UserDto
    {
      Id = user.Id,
      FirstName = user.FirstName,
      LastName = user.LastName,
      Email = user.Email,
      PhoneNumber = user.PhoneNumber
    }).ToList();
    
  }
}
